using BA.Repository.Helper;
using GameWebServer.Manager;
using GameWebServer.Modules.IGWModule;
using Kosher.Extensions.Log;
using Kosher.Log;

namespace GameWebServer
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var logConfigPath = $"{AppContext.BaseDirectory}KosherLog.config";
#if DEBUG
            logConfigPath =$"{Environment.CurrentDirectory}/../Config/KosherLog.config";
#endif

            LogBuilder.Configuration(LogConfigXmlReader.Load(logConfigPath));
            var configuration = LogBuilder.Build();

            DBHelper.Build();

            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;

            var builder = WebApplication.CreateBuilder(new WebApplicationOptions() 
            {
                Args = args,
            });
#if DEBUG
            Environment.CurrentDirectory = AppContext.BaseDirectory;
#endif
            builder.Services.AddControllersWithViews();

            Dependency.Init(builder);

            var app = builder.Build();

            app.UseRouting();
            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=KeepAlive}/{action=index}/{id?}"
            );

            InterServerClientModule.Instance.Start();

            app.Run();
        }

        private static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            var exception = e.ExceptionObject as Exception;
            LogHelper.Fatal(exception);
        }
    }
}