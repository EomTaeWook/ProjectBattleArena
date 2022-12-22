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
    public class AuthRepository
    {
        readonly IDBContext _dbContext;
        public AuthRepository(IDBContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<bool> CreateAuth(AuthModel authModel, long currentTime)
        {
            try
            {
                using (MySqlConnection connection = new MySqlConnection(_dbContext.GetConnString()))
                {
                    var param = new DynamicParameters();
                    param.AddParam(authModel.Account, nameof(authModel.Account));
                    param.AddParam(authModel.Password, nameof(authModel.Password));
                    param.AddParam(currentTime, "CreatedTime");
                    CommandDefinition command = new CommandDefinition(DBHelper.GetSPName(SP.CreateAuth),
                                                                                param,
                                                                                commandType: CommandType.StoredProcedure);

                    var result = await connection.ExecuteAsync(command);
                    return result > 0;
                }
            }
            catch (Exception ex)
            {
                LogHelper.Error(ex);
                return false;
            }
        }
        public async Task<AuthModel> LoadAuth(string account)
        {
            try
            {
                using (MySqlConnection connection = new MySqlConnection(_dbContext.GetConnString()))
                {
                    var param = new DynamicParameters();
                    param.AddParam(account, nameof(account));
                    CommandDefinition command = new CommandDefinition(DBHelper.GetSPName(SP.LoadAuth),
                                                                                param,
                                                                                commandType: CommandType.StoredProcedure);

                    var result = await connection.QueryFirstOrDefaultAsync<AuthModel>(command);

                    if (result != null)
                    {
                        return result;
                    }
                    return new AuthModel();
                }
            }
            catch (Exception ex)
            {
                LogHelper.Error(ex);
                return null;
            }
        }
    }
}
