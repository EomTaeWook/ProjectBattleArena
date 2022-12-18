using BA.Repository;
using Dapper;
using Kosher.Log;
using MySql.Data.MySqlClient;
using Protocol.GameWebServerAndClient.ShareModel;
using Repository.Interface;
using System.Data;

namespace Repository
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
                    param.AddParam(characterData.Job, nameof(characterData.Job));
                    param.AddParam(characterData.CreateTime, nameof(characterData.CreateTime));
                    CommandDefinition command = new CommandDefinition(DBHelper.GetSPName(DBHelper.SP.CreateCharacter),
                                                                                param,
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
        public async Task<List<CharacterData>> LoadCharacters(string account)
        {
            try
            {
                using (MySqlConnection connection = new MySqlConnection(_dbContext.GetConnString()))
                {
                    var param = new DynamicParameters();
                    param.AddParam(account, nameof(account));
                    CommandDefinition command = new CommandDefinition(DBHelper.GetSPName(DBHelper.SP.LoadCharacters),
                                                                                param,
                                                                                commandType: CommandType.StoredProcedure);

                    var result = await SqlMapper.QueryAsync<CharacterData>(connection, command);

                    return result.AsList();
                }
            }
            catch(Exception ex)
            {
                LogHelper.Error(ex);
                return null;
            }
        }
    }
}

