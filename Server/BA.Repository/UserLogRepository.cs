using BA.Models;
using BA.Repository.Helper;
using BA.Repository.Interface;
using Dapper;
using Kosher.Log;
using MySql.Data.MySqlClient;
using System.Data;
using static BA.Repository.Helper.DBHelper;

namespace BA.Repository
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
                    var param = new DynamicParameters();
                    param.AddParam(logModel.Account, nameof(logModel.Account));
                    param.AddParam(logModel.LoggedTime, nameof(logModel.LoggedTime));
                    param.AddParam(logModel.Log, nameof(logModel.Log));
                    param.AddParam(logModel.Path, nameof(logModel.Path));
                    CommandDefinition command = new CommandDefinition(DBHelper.GetSPName(SP.InsertUserLog),
                                                                        param,
                                                                        commandType: CommandType.StoredProcedure);

                    var result = await connection.ExecuteAsync(command);
                    return true;
                }
            }
            catch (Exception ex)
            {
                LogHelper.Error(ex);
                return false;
            }
        }
    }
}

