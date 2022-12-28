using BA.DedicatedServer.Modules.Stun;
using Kosher.Extensions.Log;
using Kosher.Log;

namespace BA.DedicatedServer
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

            StunModule.Instance.Start();
        }

        private static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            var exception = e.ExceptionObject as Exception;
            LogHelper.Fatal(exception);
        }
    }
}