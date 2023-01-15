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

        public override async Task<IGWCResponse> Process(TokenData tokenData, CreateCharacter request)
        {
            if(string.IsNullOrEmpty(tokenData.CharacterName) == true)
            {
                return MakeErrorMessage(tokenData.Account, $"character name is empty");
            }

            var template = TemplateContainer<CharacterTemplate>.Find(request.CharacterTemplateId);
            if (template.Invalid())
            {
                return MakeErrorMessage(tokenData.Account, $"character job is invalid");
            }

            var loadCharacter = await _characterRepository.LoadCharacter(tokenData.CharacterName);

            if(loadCharacter == null)
            {
                return MakeErrorMessage(tokenData.Account, $"failed to load character");
            }

            if (string.IsNullOrEmpty(loadCharacter.CharacterName) == false)
            {
                return MakeErrorMessage(tokenData.Account, $"already character name");
            }

            var characterData = new CharacterData()
            {
                CharacterName = tokenData.CharacterName,
                CreateTime = DateTime.Now.Ticks,
                TemplateId = request.CharacterTemplateId,
            };
            var created = await _characterRepository.CreateCharacter(characterData, tokenData.Account);

            if(created == false)
            {
                return MakeErrorMessage(tokenData.Account, $"failed to created character");
            }

            created = await _skillRepository.CreateMountingSkill(tokenData.CharacterName,
                characterData.CreateTime);
            if (created == false)
            {
                return MakeErrorMessage(tokenData.Account, $"failed to created mounting skill");
            }

            return new CreateCharacterResponse()
            {
                Ok = true,
                NewCharacterData = characterData
            };
        }
    }
}
