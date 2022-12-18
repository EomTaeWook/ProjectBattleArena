using Repository.Interface;

namespace GameWebServer.Models
{
    public class DBContext : IDBContext
    {
        private readonly string _connString;
        public DBContext(DBConfig config)
        {
            _connString = $"Server={config.EndPoint}; Port={config.Port}; Database={config.Database}; Uid={config.UserId}; Pwd={config.Password};";
        }
        public string GetConnString()
        {
            return _connString;
        }
    }
}
