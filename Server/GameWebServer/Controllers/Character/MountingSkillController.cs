using BA.Repository;
using DataContainer.Generated;
using Protocol.GameWebServerAndClient;
using Protocol.GameWebServerAndClient.ShareModels;
using ShareLogic;
using TemplateContainers;

namespace GameWebServer.Controllers.Character
{
    public class MountingSkillController : AuthAPIController<MountingSkill>
    {
        private readonly SkillRepository _skillRepository;
        private readonly CharacterRepository _characterRepository;
        public MountingSkillController(SkillRepository skillRepository,
            CharacterRepository characterRepository)
        {
            _skillRepository = skillRepository;
            _characterRepository = characterRepository;
        }

        public override async Task<IGWCResponse> Process(TokenData tokenData, MountingSkill request)
        {
            var loadCharacter = await _characterRepository.LoadCharacter(tokenData.CharacterName);

            if(loadCharacter == null)
            {
                return MakeErrorMessage(tokenData.Account, $"not found character");
            }

            var slotIndexLevel = TemplateContainer<ConstantTemplate>.Find($"OpenSlot{request.SlotIndex}");
            var characterLevel = LevelUpHelper.GetLevel(loadCharacter.Exp);

            if(slotIndexLevel.Value > characterLevel)
            {
                return MakeErrorMessage(tokenData.Account, $"character level is low");
            }

            var skillData = await _skillRepository.LoadSkillByCharacterNameAndId(request.SkillId,
                tokenData.CharacterName);

            if(skillData == null)
            {
                return MakeErrorMessage(tokenData.Account, $"not found skill data");
            }

            var loadMounting = await _skillRepository.LoadMountingSkillByCharacterName(tokenData.CharacterName);
            if(loadMounting == null)
            {
                return MakeErrorMessage(tokenData.Account, $"failed to load Mounting skill");
            }

            var currentId = loadMounting.GetSlotId(request.SlotIndex);

            var updated = await _skillRepository.UpdateMountingSkill(tokenData.CharacterName,
                currentId,
                request.SkillId,
                request.SlotIndex,
                DateTime.Now.Ticks);

            if(updated == false)
            {
                return MakeErrorMessage(tokenData.Account, $"failed to update equipment skill");
            }

            return new MountingSkillResponse()
            {
                Ok = true,
                SlotIndex = request.SlotIndex,
                ChangedSkillId = request.SkillId
            };
        }
    }
}
