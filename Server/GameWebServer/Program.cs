using BA.Repository;
using GameWebServer.Manager;
using GameWebServer.Models;
using Kosher.Extensions.Log;
using Kosher.Log;
using Kosher.Log.LogTarget;
using Repository;
using Repository.Interface;
using System.Data;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace GameWebServer
{
    public class Program
    {
        public static void Main(string[] args)
        {
            LogBuilder.Configuration(LogConfigXmlReader.Load($"{AppContext.BaseDirectory}KosherLog.config"));
            var configuration = LogBuilder.Build();

            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;

            var builder = WebApplication.CreateBuilder(new WebApplicationOptions() 
            {
                Args = args,
            });
#if DEBUG
            Environment.CurrentDirectory = AppContext.BaseDirectory;
#endif
            builder.Services.AddControllersWithViews();

            SetDependency(builder);

            var app = builder.Build();
            app.UseRouting();
            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=KeepAlive}/{action=index}/{id?}"
            );
            app.Run();
        }

        private static void SetDependency(WebApplicationBuilder builder)
        {
            var configPath = $"{AppContext.BaseDirectory}\\config.json";
#if DEBUG
            configPath = $"{builder.Environment.ContentRootPath}\\config.json";
#endif
            var json = File.ReadAllText(configPath);

            var config = JsonSerializer.Deserialize<Config>(json);

            var gameDBContext = new DBContext(config.GameDB);

            var logDBContext = new DBContext(config.LogDB);

            builder.Services.AddSingleton<IDBContext>(gameDBContext);
            builder.Services.AddSingleton<ILogDBContext>(logDBContext);

            builder.Services.AddTransient<AuthRepository, AuthRepository>();
            builder.Services.AddTransient<LogRepository, LogRepository>();

            ActionLogManager.Instance.Init(logDBContext);
            DBHelper.Build();
        }


        private static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            var exception = e.ExceptionObject as Exception;
            LogHelper.Fatal(exception);
        }
    }
}