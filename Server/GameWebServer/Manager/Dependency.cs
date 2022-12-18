using GameWebServer.Models;
using Repository;
using Repository.Interface;
using System.Text.Json;

namespace GameWebServer.Manager
{
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
            builder.Services.AddTransient<AuthRepository, AuthRepository>();
            builder.Services.AddTransient<UserLogRepository, UserLogRepository>();
            builder.Services.AddTransient<CharacterRepository, CharacterRepository>();
            builder.Services.AddTransient<UserAssetRepository, UserAssetRepository>();

            UserLogManager.Instance.Init(gameDBContext);
        }
    }
}
