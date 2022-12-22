﻿using BA.Repository.Helper;
using BA.Repository.Interface;
using GameWebServer.Models;
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
            
            foreach (var item in DBServiceHelper.GetServiceTypes())
            {
                builder.Services.AddTransient(item.Key, item.Value);
            }
            DBServiceHelper.AddSingleton<IDBContext>(gameDBContext);
            builder.Services.AddSingleton<IDBContext>(gameDBContext);
        }
    }
}
