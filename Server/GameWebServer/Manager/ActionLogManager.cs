using BA.Models;
using GameWebServer.Models;
using Kosher.Framework;
using Protocol.GameWebServerAndClient;
using Repository;
using System.Text.Json;

namespace GameWebServer.Manager
{
    public class UserLogManager : Singleton<UserLogManager>
    {
        UserLogRepository _logRepository;
        public void Init(DBContext dbContext)
        {
            _logRepository = new UserLogRepository(dbContext);
        }
        public async Task InsertLogAsync(string account, string path, IGWCResponse response)
        {
            var logModel = new UserLogModel()
            {
                Account = account,
                LoggedTime = DateTime.Now.Ticks,
                Path = path,
                Log = JsonSerializer.Serialize(response)
            };

            await _logRepository.InsertUserLog(logModel);
        }
    }
}
