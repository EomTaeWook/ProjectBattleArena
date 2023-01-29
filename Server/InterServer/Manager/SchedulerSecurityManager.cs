using BA.InterServer.Models;
using BA.InterServer.ServerModule;
using BA.Repository;
using BA.Repository.Helper;
using BA.Repository.Interface;
using Kosher.Framework;
using Kosher.Log;
using Protocol.InterAndGWS;
using Protocol.InterAndGWS.ShareModels;
using ShareLogic;
using ShareLogic.Network;
using System.Text.Json;

namespace BA.InterServer.Manager
{
    internal class SchedulerSecurityManager : Singleton<SchedulerSecurityManager>
    {
        
        public string LatestPrivateKey { get; private set; }
        private List<string> _gameWebServerEndPoint = new List<string>();
        private HttpRequester _httpRequester = new HttpRequester();
        public void Init()
        {
            var configPath = $"{AppContext.BaseDirectory}//InterServerConfig.json";
#if DEBUG
            configPath = $"{Environment.CurrentDirectory}../../../../../Config/InterServerConfig.json";
#endif
            var json = File.ReadAllText(configPath);

            var config = JsonSerializer.Deserialize<Config>(json);

            var gameDBContext = new DBContext(config.GameDB);
            DBServiceHelper.AddSingleton<IDBContext>(gameDBContext);

            _gameWebServerEndPoint.AddRange(config.GWSEndPoint);
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
        public List<string> GetGWSEndPoint()
        {
            return _gameWebServerEndPoint;
        }
        public async Task<bool> CreateKey()
        {
            SecurityRepository securityRepository = DBServiceHelper.GetService<SecurityRepository>();

            var cryptoKeyString = Cryptogram.GetKeyString();

            var created = await securityRepository.InsertSecurityKey(new BA.Models.SecurityKeyModel()
            {
                CreatedTime = DateTime.Now.Ticks,
                PrivateKey = cryptoKeyString.Item1,
                PublicKey = cryptoKeyString.Item2
            });

            var packetData = new ChangedSecurityKey
            {
                PrivateKey = cryptoKeyString.Item1
            };

            var packet = Packet.MakePacket((ushort)IGWSProtocol.GameWebServerInspection, packetData);

            InterServerModule.Instance.Broadcast(packet);

            LatestPrivateKey = cryptoKeyString.Item1;

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

            try
            {
                foreach (var item in _gameWebServerEndPoint)
                {
                    var response = await _httpRequester.PostByJsonAsync(item, "");
                    LogHelper.Debug($"send {item}");
                }
            }
            catch(Exception ex)
            {
                LogHelper.Fatal(ex);
                LogHelper.Debug($"game web server is not response! : {ex.Message}");
            }
            

            await Task.Delay(60000);
            _ = Start();
        }
    }
}
