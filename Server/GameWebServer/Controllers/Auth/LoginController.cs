using Protocol.GameWebServerAndClient;
using Protocol.GameWebServerAndClient.ShareModel;
using Repository;

namespace GameWebServer.Controllers.Auth
{
    public class LoginController : APIController<Login>
    {
        private AuthRepository _authRepository;
        private CharacterRepository _characterRepository;
        public LoginController(AuthRepository authRepository,
                            CharacterRepository characterRepository)
        {
            _authRepository = authRepository;
            _characterRepository = characterRepository;
        }
        public override async Task<IGWCResponse> Process(Login request)
        {
            if(string.IsNullOrEmpty(request.Account) == true)
            {
                return MakeErrorMessage(request.Account, "account is empty");
            }
            if (string.IsNullOrEmpty(request.Password) == true)
            {
                return MakeErrorMessage(request.Account, "password is empty");
            }

            var loadModel = await _authRepository.LoadAuth(request.Account);

            if (loadModel == null)
            {
                return MakeErrorMessage(request.Account, "not found account");
            }
            if(string.IsNullOrEmpty(loadModel.Account) == true)
            {
                return MakeErrorMessage(request.Account, $"failed to load account");
            }
            if(loadModel.Password.Equals(request.Password) == false)
            {
                return MakeErrorMessage(request.Account, $"failed to login {request.Account} invalid password");
            }

            var loadCharacter = await _characterRepository.LoadCharacters(request.Account);

            if(loadCharacter == null)
            {
                return MakeErrorMessage(request.Account, $"failed to load characters");
            }



            return new LoginResponse()
            {
                Ok = true,
                CharacterDatas = loadCharacter
            };
        }
    }
}
