using BA.InterServer.Manager;
using BA.InterServer.ServerModule;
using BA.Repository.Helper;
using Kosher.Extensions.Log;
using Kosher.Log;

namespace InterServer
{
    internal class Program
    {
        static void Main(string[] args)
        {
            LogBuilder.Configuration(LogConfigXmlReader.Load($"{AppContext.BaseDirectory}KosherLog.config"));
            var configuration = LogBuilder.Build();

            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;

            DBHelper.Build();
            SchedulerSecurityManager.Instance.Init();

            var task = SchedulerSecurityManager.Instance.Start();
            CommandManager.Instance.Run();

            InterServerModule.Instance.Start();
        }

        private static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            var exception = e.ExceptionObject as Exception;
            LogHelper.Fatal(exception);
        }
    }
}