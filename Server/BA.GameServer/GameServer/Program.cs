using BA.GameServer.Modules.Game;
using BA.GameServer.Modules.Stun;
using BA.Repository.Helper;
using DataContainer.Generated;
using Kosher.Extensions.Log;
using Kosher.Log;

namespace BA.GameServer
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
            var configuration = LogBuilder.Build();

            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException; ;

            TemplateLoad();

            DBHelper.Build();

            GameServerModule.Init();
            GameServerModule.Instance.Start();

            StunModule.Instance.Start();
        }

        private static void TemplateLoad()
        {
            var path = "../../Datas";
#if DEBUG
            path = $"{AppContext.BaseDirectory}../../../../../../Datas";
#endif
            TemplateLoader.Load(path);
        }

        private static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            var exception = e.ExceptionObject as Exception;
            LogHelper.Fatal(exception);
        }
    }
}