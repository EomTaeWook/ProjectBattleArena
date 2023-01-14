using BA.Repository;
using DataContainer.Generated;
using Protocol.GameWebServerAndClient;
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

        public override async Task<IGWCResponse> Process(string account, MountingSkill request)
        {
            if(string.IsNullOrEmpty(request.CharacterName) == true)
            {
                return MakeErrorMessage(account, $"character name is empty");
            }

            var loadCharacter = await _characterRepository.LoadCharacter(request.CharacterName);

            if(loadCharacter == null)
            {
                return MakeErrorMessage(account, $"not found character");
            }

            var slotIndexLevel = TemplateContainer<ConstantTemplate>.Find($"OpenSlot{request.SlotIndex}");
            var characterLevel = LevelUpHelper.GetLevel(loadCharacter.Exp);

            if(slotIndexLevel.Value > characterLevel)
            {
                return MakeErrorMessage(account, $"character level is low");
            }

            var skillData = await _skillRepository.LoadSkillByCharacterNameAndId(request.SkillId,
                request.CharacterName);

            if(skillData == null)
            {
                return MakeErrorMessage(account, $"not found skill data");
            }

            var loadMounting = await _skillRepository.LoadMountingSkillByCharacterName(request.CharacterName);
            if(loadMounting == null)
            {
                return MakeErrorMessage(account, $"failed to load Mounting skill");
            }

            var currentId = loadMounting.GetSlotId(request.SlotIndex);

            var updated = await _skillRepository.UpdateMountingSkill(request.CharacterName,
                currentId,
                request.SkillId,
                request.SlotIndex,
                DateTime.Now.Ticks);

            if(updated == false)
            {
                return MakeErrorMessage(account, $"failed to update equipment skill");
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
