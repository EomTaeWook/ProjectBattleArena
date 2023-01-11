using BA.Repository;
using DataContainer.Generated;
using Protocol.GameWebServerAndClient;
using TemplateContainers;

namespace GameWebServer.Controllers.Character
{
    public class EquipmentSkillController : AuthAPIController<EquipmentSkill>
    {
        private SkillRepository _skillRepository;
        private CharacterRepository _characterRepository;
        public EquipmentSkillController(SkillRepository skillRepository,
            CharacterRepository characterRepository)
        {
            _skillRepository = skillRepository;
            _characterRepository = characterRepository;
        }

        public override async Task<IGWCResponse> Process(string account, EquipmentSkill request)
        {
            if(string.IsNullOrEmpty(request.CharacterName) == true)
            {
                return MakeErrorMessage(account, $"character name is empty");
            }

            var loadCharacter = await _characterRepository.LoadCharacter(request.CharacterName);

            if(loadCharacter == null)
            {
                return MakeErrorMessage(account, $"failed to load character");
            }

            var skillTemplate = TemplateContainer<SkillsTemplate>.Find(request.SkillTemplateId);

            if(skillTemplate.Invalid())
            {
                return MakeErrorMessage(account, $"invalid skill template!");
            }


            var updated = await _skillRepository.EquipmentSkill(request.CharacterName, -1, request.SkillTemplateId, request.SlotIndex, DateTime.Now.Ticks);

            if(updated == false)
            {
                return MakeErrorMessage(account, $"failed to update equipment skill");
            }

            return new EquipmentSkillResponse()
            {
                Ok = true,
            };
        }
    }
}
