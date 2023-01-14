using BA.Models;
using BA.Repository.Helper;
using BA.Repository.Interface;
using Dapper;
using Kosher.Log;
using MySql.Data.MySqlClient;
using Protocol.GameWebServerAndClient.ShareModels;
using System.Data;
using static BA.Repository.Helper.DBHelper;

namespace BA.Repository
{
    public class SkillRepository
    {
        readonly IDBContext _dbContext;
        public SkillRepository(IDBContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<bool> CreateMountingSkill(string characterName, long createTime)
        {
            try
            {
                using (MySqlConnection connection = new MySqlConnection(_dbContext.GetConnString()))
                {
                    var param = new DynamicParameters();
                    param.AddParam(characterName, nameof(characterName));
                    param.AddParam(createTime, nameof(createTime));
                    CommandDefinition command = new CommandDefinition(DBHelper.GetSPName(SP.CreateMountingSkill),
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
        public async Task<long> InsertSkill(string characterName, int templateId, long createTime)
        {
            try
            {
                using (MySqlConnection connection = new MySqlConnection(_dbContext.GetConnString()))
                {
                    var param = new DynamicParameters();
                    param.AddParam(characterName, nameof(characterName));
                    param.AddParam(templateId, nameof(templateId));
                    param.AddParam(createTime, nameof(createTime));
                    param.Add("param_new_id", dbType: DbType.Int64, direction: ParameterDirection.Output);

                    CommandDefinition command = new CommandDefinition(DBHelper.GetSPName(SP.InsertSkill),
                                                                                param,
                                                                                commandType: CommandType.StoredProcedure);

                    var result = await connection.ExecuteAsync(command);

                    var newId = param.Get<long>("param_new_id");
                    return newId;
                }
            }
            catch (Exception ex)
            {
                LogHelper.Error(ex);
                return -1;
            }
        }
        public async Task<List<SkillData>> LoadSkillByCharacterName(string characterName)
        {
            try
            {
                using (MySqlConnection connection = new MySqlConnection(_dbContext.GetConnString()))
                {
                    var param = new DynamicParameters();
                    param.AddParam(characterName, nameof(characterName));
                    CommandDefinition command = new CommandDefinition(DBHelper.GetSPName(SP.LoadSkillByCharacterName),
                                                                                param,
                                                                                commandType: CommandType.StoredProcedure);

                    var result = await connection.QueryAsync<SkillData>(command);
                    return result.AsList();
                }
            }
            catch (Exception ex)
            {
                LogHelper.Error(ex);
                return null;
            }
        }
        public async Task<SkillData> LoadSkillByCharacterNameAndId(long id, string characterName)
        {
            try
            {
                using (MySqlConnection connection = new MySqlConnection(_dbContext.GetConnString()))
                {
                    var param = new DynamicParameters();
                    param.AddParam(characterName, nameof(characterName));
                    param.AddParam(id, nameof(id));
                    CommandDefinition command = new CommandDefinition(DBHelper.GetSPName(SP.LoadSkillByCharacterNameAndId),
                                                                                param,
                                                                                commandType: CommandType.StoredProcedure);

                    var result = await connection.QueryAsync<SkillData>(command);
                    return result.FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                LogHelper.Error(ex);
                return null;
            }
        }
        public async Task<MountingSkillModel> LoadMountingSkillByCharacterName(string characterName)
        {
            try
            {
                using (MySqlConnection connection = new MySqlConnection(_dbContext.GetConnString()))
                {
                    var param = new DynamicParameters();
                    param.AddParam(characterName, nameof(characterName));
                    CommandDefinition command = new CommandDefinition(DBHelper.GetSPName(SP.LoadMountingSkillByCharacterName),
                                                                                param,
                                                                                commandType: CommandType.StoredProcedure);

                    var result = await connection.QueryAsync<MountingSkillModel>(command);
                    if(result.Count() > 0)
                    {
                        return result.First();
                    }
                    return new MountingSkillModel()
                    {
                        Slot1 = -1,
                        Slot2 = -1,
                        Slot3 = -1,
                        Slot4 = -1
                    };
                }
            }
            catch (Exception ex)
            {
                LogHelper.Error(ex);
                return null;
            }
        }
        public async Task<bool> UpdateMountingSkill(string characterName,
            long currentSkillId,
            long updateSkillId,
            int slotIndex,
            long updateTime)
        {
            try
            {
                using (MySqlConnection connection = new MySqlConnection(_dbContext.GetConnString()))
                {
                    var param = new DynamicParameters();
                    param.AddParam(characterName, nameof(characterName));
                    param.AddParam(currentSkillId, nameof(currentSkillId));
                    param.AddParam(updateSkillId, nameof(updateSkillId));
                    param.AddParam(updateTime, nameof(updateTime));
                    param.AddParam(slotIndex + 1, nameof(slotIndex));
                    CommandDefinition command = new CommandDefinition(DBHelper.GetSPName(SP.UpdateMountingSkill),
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
        public async Task<bool> UnMountingSkill(string characterName, int slotIndex)
        {
            try
            {
                int skillTemplate = -1;
                using (MySqlConnection connection = new MySqlConnection(_dbContext.GetConnString()))
                {
                    var param = new DynamicParameters();
                    param.AddParam(characterName, nameof(characterName));
                    param.AddParam(skillTemplate, nameof(skillTemplate));
                    param.Add("param_slot_index", $"slot_{slotIndex}");
                    CommandDefinition command = new CommandDefinition(DBHelper.GetSPName(SP.UnMountingSkill),
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

