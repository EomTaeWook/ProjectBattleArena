using BA.InternalServer.Modules.Net;
using BA.InterServer.CLICommand;
using BA.InterServer.Manager;
using BA.InterServer.ServerModule;
using BA.Repository.Helper;
using CLISystem;
using Kosher.Extensions.Log;
using Kosher.Log;
using Kosher.Sockets;
using Protocol.InterAndGWS;

namespace InterServer
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var logConfigPath = $"{AppContext.BaseDirectory}KosherLog.config";
#if DEBUG
            logConfigPath = $"{AppContext.BaseDirectory}../../../../Config/KosherLog.config";
#endif
            LogBuilder.Configuration(LogConfigXmlReader.Load(logConfigPath));
            LogBuilder.Build();

            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;

            DBHelper.Build();

            var configPath = $"{AppContext.BaseDirectory}//InternalServerConfig.json";
#if DEBUG
            configPath = $"{Environment.CurrentDirectory}../../../../../Config/InternalServerConfig.json";
#endif
            SchedulerSecurityManager.Instance.Init(configPath);

            HandlerBinder<GWSIProtocolHandler, string>.Bind<GWSIProtocol>();

            var task = SchedulerSecurityManager.Instance.Start();

            var cli = new NetCLIModule();
            cli.AddCmdProcessor<GwsOnOff>();
            cli.AddCmdProcessor<Close>();
            cli.AddCmdProcessor<ResetKey>();
            cli.Build();
            cli.Run(31100);

            InternalServerModule.Instance.Start();
        }

        private static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            var exception = e.ExceptionObject as Exception;
            LogHelper.Fatal(exception);
        }
    }
}