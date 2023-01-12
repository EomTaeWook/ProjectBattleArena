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
        public async Task<bool> CreateEquipmentSkill(string characterName, long createTime)
        {
            try
            {
                using (MySqlConnection connection = new MySqlConnection(_dbContext.GetConnString()))
                {
                    var param = new DynamicParameters();
                    param.AddParam(characterName, nameof(characterName));
                    param.AddParam(createTime, nameof(createTime));
                    CommandDefinition command = new CommandDefinition(DBHelper.GetSPName(SP.CreateEquipmentSkill),
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
        public async Task<bool> InsertSkill(string characterName, int templateId, long createTime)
        {
            try
            {
                using (MySqlConnection connection = new MySqlConnection(_dbContext.GetConnString()))
                {
                    var param = new DynamicParameters();
                    param.AddParam(characterName, nameof(characterName));
                    param.AddParam(templateId, nameof(templateId));
                    param.AddParam(createTime, nameof(createTime));
                    CommandDefinition command = new CommandDefinition(DBHelper.GetSPName(SP.InsertSkill),
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
        public async Task<bool> EquipmentSkill(string characterName,
            int currentTemplateId,
            int updateTemplateId,
            int slotIndex,
            long updateTime)
        {
            try
            {
                using (MySqlConnection connection = new MySqlConnection(_dbContext.GetConnString()))
                {
                    var param = new DynamicParameters();
                    param.AddParam(characterName, nameof(characterName));
                    param.AddParam(currentTemplateId, nameof(currentTemplateId));
                    param.AddParam(updateTemplateId, nameof(updateTemplateId));
                    param.AddParam(updateTime, nameof(updateTime));
                    param.Add("param_slot_index", $"slot_{slotIndex}");
                    CommandDefinition command = new CommandDefinition(DBHelper.GetSPName(SP.EquipmentSkill),
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
        public async Task<bool> UnEquipmentSkill(string characterName, int slotIndex)
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
                    CommandDefinition command = new CommandDefinition(DBHelper.GetSPName(SP.UnEquipmentSkill),
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

