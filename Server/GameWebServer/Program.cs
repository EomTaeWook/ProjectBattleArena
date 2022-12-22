using BA.Repository;
using GameWebServer.Manager;
using Kosher.Extensions.Log;
using Kosher.Log;

namespace GameWebServer
{
    public class Program
    {
        public static void Main(string[] args)
        {
            LogBuilder.Configuration(LogConfigXmlReader.Load($"{AppContext.BaseDirectory}KosherLog.config"));
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
            
            app.Run();
        }

        private static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            var exception = e.ExceptionObject as Exception;
            LogHelper.Fatal(exception);
        }
    }
}