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
    public class CharacterRepository
    {
        readonly IDBContext _dbContext;
        public CharacterRepository(IDBContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<bool> CreateCharacter(CharacterData characterData, string account)
        {
            try
            {
                using (MySqlConnection connection = new MySqlConnection(_dbContext.GetConnString()))
                {
                    var param = new DynamicParameters();
                    param.AddParam(account, nameof(account));
                    param.AddParam(characterData.CharacterName, nameof(characterData.CharacterName));
                    param.AddParam(characterData.TemplateId, nameof(characterData.TemplateId));
                    param.AddParam(characterData.CreateTime, nameof(characterData.CreateTime));
                    CommandDefinition command = new CommandDefinition(DBHelper.GetSPName(SP.CreateCharacter),
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
        public async Task<CharacterData> LoadCharacter(string characterName)
        {
            try
            {
                using (MySqlConnection connection = new MySqlConnection(_dbContext.GetConnString()))
                {
                    var param = new DynamicParameters();
                    param.AddParam(characterName, nameof(characterName));
                    CommandDefinition command = new CommandDefinition(DBHelper.GetSPName(SP.LoadCharacterByCharacterName),
                                                                                param,
                                                                                commandType: CommandType.StoredProcedure);

                    var result = await connection.QueryFirstOrDefaultAsync<CharacterData>(command);
                    if (result == null)
                    {
                        result = new CharacterData();
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
        public async Task<List<CharacterData>> LoadCharacters(string account)
        {
            try
            {
                using (MySqlConnection connection = new MySqlConnection(_dbContext.GetConnString()))
                {
                    var param = new DynamicParameters();
                    param.AddParam(account, nameof(account));
                    CommandDefinition command = new CommandDefinition(DBHelper.GetSPName(SP.LoadCharacters),
                                                                            param,
                                                                            commandType: CommandType.StoredProcedure);

                    var result = await connection.QueryAsync<CharacterData>(command);

                    return result.AsList();
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

