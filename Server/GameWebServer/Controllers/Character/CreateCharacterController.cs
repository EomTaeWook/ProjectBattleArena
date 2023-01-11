using BA.Repository;
using DataContainer.Generated;
using Protocol.GameWebServerAndClient;
using Protocol.GameWebServerAndClient.ShareModels;
using TemplateContainers;

namespace GameWebServer.Controllers.Character
{
    public class CreateCharacterController : AuthAPIController<CreateCharacter>
    {
        private CharacterRepository _characterRepository;
        private SkillRepository _skillRepository;
        public CreateCharacterController(CharacterRepository characterRepository,
            SkillRepository skillRepository)
        {
            _characterRepository = characterRepository;
            _skillRepository = skillRepository;
        }

        public override async Task<IGWCResponse> Process(string account, CreateCharacter request)
        {
            if(string.IsNullOrEmpty(request.CharacterName) == true)
            {
                return MakeErrorMessage(account, $"character name is empty");
            }

            var template = TemplateContainer<CharacterTemplate>.Find(request.CharacterTemplateId);
            if (template.Invalid())
            {
                return MakeErrorMessage(account, $"character job is invalid");
            }

            var loadCharacter = await _characterRepository.LoadCharacter(request.CharacterName);

            if(loadCharacter == null)
            {
                return MakeErrorMessage(account, $"failed to load character");
            }

            if (string.IsNullOrEmpty(loadCharacter.CharacterName) == false)
            {
                return MakeErrorMessage(account, $"already character name");
            }

            var characterData = new CharacterData()
            {
                CharacterName = request.CharacterName,
                CreateTime = DateTime.Now.Ticks,
                TemplateId = request.CharacterTemplateId,
            };
            var created = await _characterRepository.CreateCharacter(characterData, account);

            if(created == false)
            {
                return MakeErrorMessage(account, $"failed to created character");
            }

            created = await _skillRepository.CreateEquipmentSkill(request.CharacterName, characterData.CreateTime);
            if (created == false)
            {
                return MakeErrorMessage(account, $"failed to created equipment skill");
            }

            return new CreateCharacterResponse()
            {
                Ok = true,
                NewCharacterData = characterData
            };
        }
    }
}
