﻿using BA.InterServer.Models;
using BA.InterServer.ServerModule;
using BA.Repository;
using BA.Repository.Helper;
using BA.Repository.Interface;
using Kosher.Framework;
using Kosher.Log;
using Protocol.InterAndGWS.ShareModels;
using Protocol.InterAndGWS;
using ShareLogic;
using System.Text.Json;

namespace BA.InterServer.Manager
{
    internal class SchedulerSecurityManager : Singleton<SchedulerSecurityManager>
    {
        public string LatestPrivateKey { get; private set; }
        public void Init()
        {
            var configPath = $"{AppContext.BaseDirectory}\\config.json";
#if DEBUG
            configPath = $"{Environment.CurrentDirectory}..\\..\\..\\..\\config.json";
#endif
            var json = File.ReadAllText(configPath);

            var config = JsonSerializer.Deserialize<Config>(json);

            var gameDBContext = new DBContext(config.GameDB);
            DBServiceHelper.AddSingleton<IDBContext>(gameDBContext);
        }
        private async Task<bool> NeedCreateKey()
        {
            SecurityRepository securityRepository = DBServiceHelper.GetService<SecurityRepository>();

            var loadLatestSecurityKey = await securityRepository.LoadLatestSecurityKey();

            var createTime = new DateTime(loadLatestSecurityKey.CreatedTime);

            LatestPrivateKey = loadLatestSecurityKey.PrivateKey;

            var now = DateTime.Now;
            if(createTime.DayOfYear < now.DayOfYear)
            {
                return true;
            }

            return false;
        }

        private async Task<bool> CreateKey()
        {
            SecurityRepository securityRepository = DBServiceHelper.GetService<SecurityRepository>();

            var privateKey = Cryptogram.GetPrivateKey();

            Cryptogram.SetPrivateKey(privateKey);

            var publicKey = Cryptogram.GetPublicKey();

            var created = await securityRepository.InsertSecurityKey(new BA.Models.SecurityKeyModel()
            {
                CreatedTime = DateTime.Now.Ticks,
                PrivateKey = privateKey,
                PublicKey = publicKey
            });

            var packetData = new ChangedSecurityKey
            {
                PrivateKey = privateKey
            };

            var packet = ServerModule.Packet.MakePacket(IGWSProtocol.GameWebServerInspection, packetData);

            InterServerModule.Instance.Broadcast(packet);

            LatestPrivateKey = privateKey;

            return created;
        }

        public async Task Start()
        {
            var needMaked = await NeedCreateKey();

            if(needMaked == true)
            {
                if(await CreateKey() == false)
                {
                    LogHelper.Error($"[interServer] failed to create key");
                }
            }

            await Task.Delay(60000);
            _ = Start();
        }
    }
}
