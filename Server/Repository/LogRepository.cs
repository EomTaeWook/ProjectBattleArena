using BA.Models;
using BA.Repository;
using Dapper;
using Kosher.Log;
using MySql.Data.MySqlClient;
using Repository.Interface;
using System.Data;

namespace Repository
{
    public class LogRepository
    {
        readonly ILogDBContext _dbContext;
        public LogRepository(ILogDBContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<bool> CreateLog(LogModel logModel)
        {
            try
            {
                using (MySqlConnection connection = new MySqlConnection(_dbContext.GetConnString()))
                {
                    CommandDefinition command = new CommandDefinition(DBHelper.GetSPName(DBHelper.SP.InsertLog),
                                                                                logModel,
                                                                                commandType: CommandType.StoredProcedure);

                    var result = await SqlMapper.ExecuteAsync(connection, command);
                    return true;
                }
            }
            catch(Exception ex)
            {
                LogHelper.Error(ex);
                return false;
            }
        }
    }
}
