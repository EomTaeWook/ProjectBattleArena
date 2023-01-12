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
    public class UserAssetRepository
    {
        readonly IDBContext _dbContext;
        public UserAssetRepository(IDBContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<UserAssetModel> LoadUserAssetAsync(string account)
        {
            try
            {
                using (MySqlConnection connection = new MySqlConnection(_dbContext.GetConnString()))
                {
                    var param = new DynamicParameters();
                    param.AddParam(account, nameof(account));
                    CommandDefinition command = new CommandDefinition(DBHelper.GetSPName(SP.LoadUserAsset),
                                                                        param,
                                                                        commandType: CommandType.StoredProcedure);

                    var result = await connection.QueryFirstOrDefaultAsync<UserAssetModel>(command);
                    return result;
                }
            }
            catch (Exception ex)
            {
                LogHelper.Error(ex);
                return null;
            }
        }

        public async Task<bool> InsertUserAssetAsync(string account, long createTime)
        {
            try
            {
                using (MySqlConnection connection = new MySqlConnection(_dbContext.GetConnString()))
                {
                    var param = new DynamicParameters();
                    param.AddParam(account, nameof(account));
                    param.AddParam(account, nameof(createTime));
                    CommandDefinition command = new CommandDefinition(DBHelper.GetSPName(SP.InsertUserAsset),
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
        public async Task<bool> ModifyGold(string account, long currentGold, long updateGold)
        {
            try
            {
                using (MySqlConnection connection = new MySqlConnection(_dbContext.GetConnString()))
                {
                    var param = new DynamicParameters();
                    param.AddParam(account, nameof(account));
                    param.AddParam(currentGold, nameof(currentGold));
                    param.AddParam(updateGold, nameof(updateGold));
                    CommandDefinition command = new CommandDefinition(DBHelper.GetSPName(SP.UpdateGold),
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
        public async Task<bool> ModifyGachaSkill(string account, int currentValue, int updateValue)
        {
            try
            {
                using (MySqlConnection connection = new MySqlConnection(_dbContext.GetConnString()))
                {
                    var param = new DynamicParameters();
                    param.AddParam(account, nameof(account));
                    param.AddParam(currentValue, nameof(currentValue));
                    param.AddParam(updateValue, nameof(updateValue));
                    CommandDefinition command = new CommandDefinition(DBHelper.GetSPName(SP.UpdateGachaSkill),
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
        public async Task<bool> ModifyCash(string account, int currentCash, int updateCash)
        {
            try
            {
                using (MySqlConnection connection = new MySqlConnection(_dbContext.GetConnString()))
                {
                    var param = new DynamicParameters();
                    param.AddParam(account, nameof(account));
                    param.AddParam(currentCash, nameof(currentCash));
                    param.AddParam(updateCash, nameof(updateCash));
                    CommandDefinition command = new CommandDefinition(DBHelper.GetSPName(SP.UpdateCash),
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
        public async Task<bool> ModifyArenaTicket(string account, int currentTicket, int updateTicket, long latestUpdateTime)
        {
            try
            {
                using (MySqlConnection connection = new MySqlConnection(_dbContext.GetConnString()))
                {
                    var param = new DynamicParameters();
                    param.AddParam(account, nameof(account));
                    param.AddParam(currentTicket, nameof(currentTicket));
                    param.AddParam(updateTicket, nameof(updateTicket));
                    param.AddParam(latestUpdateTime, nameof(latestUpdateTime));
                    CommandDefinition command = new CommandDefinition(DBHelper.GetSPName(SP.UpdateArenaTicket),
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
    }
}
