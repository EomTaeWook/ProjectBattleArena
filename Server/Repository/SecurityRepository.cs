using BA.Models;
using BA.Repository;
using Dapper;
using Kosher.Log;
using MySql.Data.MySqlClient;
using Protocol.GameWebServerAndClient.ShareModel;
using Repository.Interface;
using System.Data;

namespace Repository
{
    public class SecurityRepository
    {
        readonly IDBContext _dbContext;
        public SecurityRepository(IDBContext dbContext)
        {
            _dbContext = dbContext;
        }
        
        public async Task<string> LoadSecurityPrivateKeyAsync()
        {
            try
            {
                using (MySqlConnection connection = new MySqlConnection(_dbContext.GetConnString()))
                {
                    var param = new DynamicParameters();
                    CommandDefinition command = new CommandDefinition(DBHelper.GetSPName(DBHelper.SP.LoadSecurityPrivateKey),
                                                                                param,
                                                                                commandType: CommandType.StoredProcedure);

                    var result = await SqlMapper.QueryFirstOrDefaultAsync<string>(connection, command);
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
                    CommandDefinition command = new CommandDefinition(DBHelper.GetSPName(DBHelper.SP.LoadSecurityPublicKey),
                                                                                param,
                                                                                commandType: CommandType.StoredProcedure);

                    var result = await SqlMapper.QueryFirstOrDefaultAsync<string>(connection, command);
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

