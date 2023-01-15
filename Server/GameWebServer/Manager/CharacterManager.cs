using BA.Repository;
using BA.Repository.Helper;
using Kosher.Framework;
using Kosher.Log;
using Protocol.GameWebServerAndClient.ShareModels;

namespace GameWebServer.Manager
{
    public class CharacterManager : Singleton<CharacterManager>
    {
        private readonly CharacterRepository _characterRepository;
        private readonly SkillRepository _skillRepository;
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
                LogHelper.Error($"failed to load characters");
                return null;
            }

            foreach(var item in loadCharacters)
            {
                if (await SettingSkillsAsync(item) == false)
                {
                    return null;
                }
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

            if(await SettingSkillsAsync(loadCharacter) == false)
            {
                return null;
            }

            return loadCharacter;
        }
        private async Task<bool> SettingSkillsAsync(CharacterData characterData)
        {
            characterData.SkillDatas = new List<SkillData>();

            characterData.MountingSkillDatas = new List<long>();

            var loadSkills = await _skillRepository.LoadSkillByCharacterName(characterData.CharacterName);

            if (loadSkills == null)
            {
                LogHelper.Error($"failed to load skills");
                return false;
            }
            characterData.SkillDatas.AddRange(loadSkills);

            var loadMountingSkill = await _skillRepository.LoadMountingSkillByCharacterName(characterData.CharacterName);

            if (loadMountingSkill == null)
            {
                LogHelper.Error($"failed to load mounting skills");
                return false;
            }

            characterData.MountingSkillDatas.Add(loadMountingSkill.Slot1);
            characterData.MountingSkillDatas.Add(loadMountingSkill.Slot2);
            characterData.MountingSkillDatas.Add(loadMountingSkill.Slot3);
            characterData.MountingSkillDatas.Add(loadMountingSkill.Slot4);

            return true;
        }
        public async Task<CharacterData> LoadCharacterByUniqueIdAsync(string uid)
        {
            var loadCharacter = await _characterRepository.LoadCharacterByUniqueId(uid);

            if (loadCharacter == null)
            {
                return null;
            }

            if (await SettingSkillsAsync(loadCharacter) == true)
            {
                return null;
            }

            return loadCharacter;
        }
    }
}
