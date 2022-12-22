using BA.Repository;
using Protocol.GameWebServerAndClient;
using System.Security.Principal;

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
            var tokenData = ValidateToken(request.Token);

            if(tokenData == null)
            {
                return MakeCommonErrorMessage("invalid request");
            }
            var loadModel = await _authRepository.LoadAuth(tokenData.Account);

            if (loadModel == null)
            {
                return MakeErrorMessage(tokenData.Account, "not found account");
            }

            if(string.IsNullOrEmpty(loadModel.Account) == true)
            {
                return MakeErrorMessage(tokenData.Account, $"failed to load account");
            }

            if(loadModel.Password.Equals(tokenData.Password) == false)
            {
                return MakeErrorMessage(tokenData.Account, $"failed to login {tokenData.Account} invalid password");
            }

            var loadCharacter = await _characterRepository.LoadCharacters(tokenData.Account);

            if(loadCharacter == null)
            {
                return MakeErrorMessage(tokenData.Account, $"failed to load characters");
            }

            var loadUserAsset = await _userAssetRepository.LoadUserAssetAsync(tokenData.Account);

            if(loadUserAsset == null)
            {
                return MakeErrorMessage(tokenData.Account, $"failed to load user asset");
            }

            return new LoginResponse()
            {
                Ok = true,
                CharacterDatas = loadCharacter
            };
        }
    }
}
