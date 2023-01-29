using BA.InterServer.CLICommand;
using BA.InterServer.Manager;
using BA.InterServer.ServerModule;
using BA.Repository.Helper;
using CLISystem;
using Kosher.Extensions.Log;
using Kosher.Log;

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
            SchedulerSecurityManager.Instance.Init();

            var task = SchedulerSecurityManager.Instance.Start();

            var cli = new NetCLIModule();
            cli.AddCmdProcessor<GwsOnOff>();
            cli.AddCmdProcessor<Close>();
            cli.AddCmdProcessor<ResetKey>();
            cli.Build();
            cli.Run(31100);

            InterServerModule.Instance.Start();
        }

        private static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            var exception = e.ExceptionObject as Exception;
            LogHelper.Fatal(exception);
        }
    }
}