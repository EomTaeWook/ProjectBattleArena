using Protocol.GameWebServerAndClient;
using Protocol.GameWebServerAndClient.ShareModel;
using Repository;

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
