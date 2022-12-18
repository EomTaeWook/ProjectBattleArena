using BA.Models;
using BA.Repository;
using Dapper;
using Kosher.Log;
using MySql.Data.MySqlClient;
using Repository.Interface;
using System.Data;

namespace Repository
{
    public class UserLogRepository
    {
        readonly IDBContext _dbContext;
        public UserLogRepository(IDBContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<bool> InsertUserLog(UserLogModel logModel)
        {
            try
            {
                using (MySqlConnection connection = new MySqlConnection(_dbContext.GetConnString()))
                {
                    CommandDefinition command = new CommandDefinition(DBHelper.GetSPName(DBHelper.SP.InsertUserLog),
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
