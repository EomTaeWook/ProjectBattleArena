using BA.Models;
using Kosher.Framework;
using Protocol.GameWebServerAndClient;
using Repository;
using System.Text.Json;

namespace GameWebServer.Manager
{
    public class UserLogManager : Singleton<UserLogManager>
    {
        public async Task InsertLogAsync(string account, string path, IGWCResponse response)
        {
            var logRepository = ServiceProvidorHelper.GetService<UserLogRepository>();
            var logModel = new UserLogModel()
            {
                Account = account,
                LoggedTime = DateTime.Now.Ticks,
                Path = path,
                Log = JsonSerializer.Serialize(response)
            };
            await logRepository.InsertUserLog(logModel);
        }
        public async Task InsertLogAsync<T>(string account, string path, T model)
        {
            var logRepository = ServiceProvidorHelper.GetService<UserLogRepository>();
            var logModel = new UserLogModel()
            {
                Account = account,
                LoggedTime = DateTime.Now.Ticks,
                Path = path,
                Log = JsonSerializer.Serialize(model)
            };
            await logRepository.InsertUserLog(logModel);
        }
    }
}
