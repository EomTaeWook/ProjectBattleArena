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
    public class SecurityRepository
    {
        readonly IDBContext _dbContext;
        public SecurityRepository(IDBContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<bool> InsertSecurityKey(SecurityKeyModel securityKeyModel)
        {
            try
            {
                using (MySqlConnection connection = new MySqlConnection(_dbContext.GetConnString()))
                {
                    var param = new DynamicParameters();
                    param.AddParam(securityKeyModel.PrivateKey, nameof(securityKeyModel.PrivateKey));
                    param.AddParam(securityKeyModel.PublicKey, nameof(securityKeyModel.PublicKey));
                    param.AddParam(securityKeyModel.CreatedTime, nameof(securityKeyModel.CreatedTime));

                    CommandDefinition command = new CommandDefinition(DBHelper.GetSPName(SP.InsertSecurityKey),
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
        public async Task<SecurityKeyModel> LoadLatestSecurityKey()
        {
            try
            {
                using (MySqlConnection connection = new MySqlConnection(_dbContext.GetConnString()))
                {
                    var param = new DynamicParameters();
                    CommandDefinition command = new CommandDefinition(DBHelper.GetSPName(SP.LoadLatestSecurityKey),
                                                                                param,
                                                                                commandType: CommandType.StoredProcedure);

                    var result = await connection.QueryFirstOrDefaultAsync<SecurityKeyModel>(command);
                    if(result == null)
                    {
                        result = new SecurityKeyModel();
                    }
                    return result;
                }
            }
            catch (Exception ex)
            {
                LogHelper.Error(ex);
                return null;
            }
        }
        public async Task<string> LoadSecurityPublicKeyAsync()
        {
            try
            {
                using (MySqlConnection connection = new MySqlConnection(_dbContext.GetConnString()))
                {
                    var param = new DynamicParameters();
                    CommandDefinition command = new CommandDefinition(DBHelper.GetSPName(SP.LoadSecurityPublicKey),
                                                                                param,
                                                                                commandType: CommandType.StoredProcedure);

                    var result = await connection.QueryFirstOrDefaultAsync<string>(command);
                    return result;
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

