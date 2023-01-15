using BA.Repository;
using GameWebServer.Manager;
using Protocol.GameWebServerAndClient;
using Protocol.GameWebServerAndClient.ShareModels;

namespace GameWebServer.Controllers.Auth
{
    public class LoginController : APIController<Login>
    {
        private AuthRepository _authRepository;
        private UserAssetRepository _userAssetRepository;
        public LoginController(AuthRepository authRepository,
                            UserAssetRepository userAssetRepository)
        {
            _authRepository = authRepository;
            _userAssetRepository = userAssetRepository;
        }
        public override async Task<IGWCResponse> Process(Login request)
        {
            if (string.IsNullOrEmpty(request.Account) == true)
            {
                return MakeCommonErrorMessage("invalid request account is empty");
            }

            var tokenData = GetTokenData(request.Token);
            if(tokenData == null)
            {
                return MakeErrorMessage(request.Account, "invalid request");
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

            if(loadModel.Password.Equals(tokenData.Password) == false)
            {
                return MakeErrorMessage(tokenData.Account, $"failed to login {request.Account} invalid password");
            }

            var loadCharacters = await CharacterManager.Instance.LoadCharacterByLoginAsync(request.Account);

            if (loadCharacters == null)
            {
                return MakeErrorMessage(request.Account, $"failed to load characters");
            }

            var assetData = await LoadUserAssetAsync(request.Account);
            if(assetData == null)
            {
                return MakeErrorMessage(request.Account, $"failed to load user asset");
            }
            
            return new LoginResponse()
            {
                Ok = true,
                CharacterDatas = loadCharacters,
                AssetData = assetData,
            };
        }
        private async Task<AssetData> LoadUserAssetAsync(string account)
        {
            var loadUserAsset = await _userAssetRepository.LoadUserAssetAsync(account);

            if (loadUserAsset == null)
            {
                return null;
            }
            var assetData = new AssetData()
            {
                ArenaTicket = loadUserAsset.ArenaTicket,
                Cash = loadUserAsset.Cash,
                GachaSkill = loadUserAsset.GachaSkill,
                Gold = loadUserAsset.Gold,
                RemainSeconds = 0
            };

            return assetData;
        }
    }
}
