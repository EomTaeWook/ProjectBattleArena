using GameWebServer.Models;
using Kosher.Framework;
using Microsoft.AspNetCore.DataProtection.KeyManagement;
using Repository.Interface;
using ShareLogic;
using System.Reflection;
using System.Text.Json;

namespace GameWebServer.Manager
{
    public class ServiceProvidorHelper : Singleton<Kosher.Framework.ServiceProvider>
    {
        private static readonly string RepositorySuffix = "Repository";
        public static void Build(IServiceCollection services)
        {
            var assemblies = Assembly.LoadFrom("BA.Repository.dll");
            foreach (var item in assemblies.GetTypes())
            {
                if(item.FullName.EndsWith(RepositorySuffix) && item.IsClass == true)
                {
                    services.AddTransient(item, item);
                    ServiceProvidorHelper.Instance.AddTransient(item, item);
                }
            }
        }
        public static void AddTransient<TService, TImplementation>() where TService : class where TImplementation : class, TService
        {
            ServiceProvidorHelper.Instance.AddTransient< TService, TImplementation>();
        }
        public static void AddSingleton<TService>(TService implementation) where TService : class
        {
            ServiceProvidorHelper.Instance.AddSingleton<TService>(implementation);
        }
        public static T GetService<T>() where T : class
        {
            return ServiceProvidorHelper.Instance.GetService<T>();
        }
    }
    public class Dependency
    {
        public static void Init(WebApplicationBuilder builder)
        {
            var configPath = $"{AppContext.BaseDirectory}\\config.json";
#if DEBUG
            configPath = $"{builder.Environment.ContentRootPath}\\config.json";
#endif
            var json = File.ReadAllText(configPath);

            var config = JsonSerializer.Deserialize<Config>(json);

            var gameDBContext = new DBContext(config.GameDB);

            builder.Services.AddSingleton<IDBContext>(gameDBContext);
            ServiceProvidorHelper.AddSingleton<IDBContext>(gameDBContext);

            ServiceProvidorHelper.Build(builder.Services);
        }
    }
}
