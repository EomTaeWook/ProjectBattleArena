using Protocol.GameWebServerAndClient;
using Repository;

namespace GameWebServer.Controllers.Auth
{
    public class LoginController : APIController<Login>
    {
        private AuthRepository _authRepository;
        private CharacterRepository _characterRepository;
        private UserAssetRepository _userAssetRepository;
        public LoginController(AuthRepository authRepository,
                            CharacterRepository characterRepository,
                            UserAssetRepository userAssetRepository)
        {
            _authRepository = authRepository;
            _characterRepository = characterRepository;
            _userAssetRepository = userAssetRepository;
        }
        public override async Task<IGWCResponse> Process(Login request)
        {
            if(string.IsNullOrEmpty(request.Token) == true)
            {
                return MakeCommonErrorMessage("token is empty");
            }
            string account = string.Empty;
            string password = string.Empty;
            var loadModel = await _authRepository.LoadAuth(account);

            if (loadModel == null)
            {
                return MakeErrorMessage(account, "not found account");
            }

            if(string.IsNullOrEmpty(loadModel.Account) == true)
            {
                return MakeErrorMessage(account, $"failed to load account");
            }

            if(loadModel.Password.Equals(password) == false)
            {
                return MakeErrorMessage(account, $"failed to login {account} invalid password");
            }

            var loadCharacter = await _characterRepository.LoadCharacters(account);

            if(loadCharacter == null)
            {
                return MakeErrorMessage(account, $"failed to load characters");
            }

            var loadUserAsset = await _userAssetRepository.LoadUserAssetAsync(account);

            if(loadUserAsset == null)
            {
                return MakeErrorMessage(account, $"failed to load user asset");
            }


            return new LoginResponse()
            {
                Ok = true,
                CharacterDatas = loadCharacter
            };
        }
    }
}
