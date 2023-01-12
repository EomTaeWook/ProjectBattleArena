using BA.Repository;
using BA.Repository.Helper;
using Kosher.Framework;
using Protocol.GameWebServerAndClient.ShareModels;

namespace GameWebServer.Manager
{
    public class CharacterManager : Singleton<CharacterManager>
    {
        private CharacterRepository _characterRepository;
        private SkillRepository _skillRepository;
        public CharacterManager()
        {
            _characterRepository = DBServiceHelper.GetService<CharacterRepository>();
            _skillRepository = DBServiceHelper.GetService<SkillRepository>();
        }
        public async Task<List<CharacterData>> LoadCharacterByLoginAsync(string account)
        {
            var loadCharacters = await _characterRepository.LoadCharacters(account);

            if (loadCharacters == null)
            {
                return null;
            }

            foreach(var item in loadCharacters)
            {
                item.SkillDatas = new List<SkillData>();

                var loadSkills = await _skillRepository.LoadSkillByCharacterName(item.CharacterName);

                if(loadSkills == null)
                {
                    return null;
                }
                item.SkillDatas.AddRange(loadSkills);
            }

            return loadCharacters;

        }
        public async Task<CharacterData> LoadCharacterAsync(string characterName)
        {
            var loadCharacter = await _characterRepository.LoadCharacter(characterName);

            if(loadCharacter == null)
            {
                return null;
            }

            return loadCharacter;
        }
    }
}
