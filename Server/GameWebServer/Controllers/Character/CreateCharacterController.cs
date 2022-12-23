using BA.Repository;
using Protocol.GameWebServerAndClient;
using Protocol.GameWebServerAndClient.ShareModels;

namespace GameWebServer.Controllers.Character
{
    public class CreateCharacterController : AuthAPIController<CreateCharacter>
    {
        private CharacterRepository _characterRepository;
        public CreateCharacterController(CharacterRepository characterRepository)
        {
            _characterRepository = characterRepository;
        }

        public override async Task<IGWCResponse> Process(string account, CreateCharacter request)
        {
            if(string.IsNullOrEmpty(request.CharacterName) == true)
            {
                return MakeErrorMessage(account, $"character name is empty");
            }
            if (request.Job == Protocol.GameWebServerAndClient.ShareModels.JobType.Max)
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
                Job = request.Job,
            };
            var created = await _characterRepository.CreateCharacter(characterData, account);

            if(created == false)
            {
                return MakeErrorMessage(account, $"failed to created character");
            }

            return new CreateCharacterResponse()
            {
                Ok = true,
                NewCharacterData = characterData
            };
        }
    }
}
