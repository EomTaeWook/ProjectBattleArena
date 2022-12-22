using BA.Models;
using BA.Repository;
using BA.Repository.Helper;
using Kosher.Framework;
using Protocol.GameWebServerAndClient;
using System.Text.Json;

namespace GameWebServer.Manager
{
    public class UserLogManager : Singleton<UserLogManager>
    {
        public async Task InsertLogAsync(string account, string path, IGWCResponse response)
        {
            var logRepository = DBServiceHelper.GetService<UserLogRepository>();
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
            var logRepository = DBServiceHelper.GetService<UserLogRepository>();
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
