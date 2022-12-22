using BA.Models;
using BA.Repository;
using GameWebServer.Manager;
using Protocol.GameWebServerAndClient;
using Protocol.GameWebServerAndClient.ShareModels;

namespace GameWebServer.Controllers.Auth
{
    public class CreateAccountController : APIController<CreateAccount>
    {
        readonly AuthRepository _authRepository;
        private UserAssetRepository _userAssetRepository;
        public CreateAccountController(AuthRepository authRepository,
            UserAssetRepository userAssetRepository)
        {
            _authRepository = authRepository;
            _userAssetRepository = userAssetRepository;
        }
        public override async Task<IGWCResponse> Process(CreateAccount request)
        {
            var loadModel = await _authRepository.LoadAuth(request.Account);

            if(loadModel == null)
            {
                return MakeCommonErrorMessage("DB Error");
            }
            if(string.IsNullOrEmpty(loadModel.Account) == false)
            {
                return MakeCommonErrorMessage("already create account");
            }

            var authModel = new AuthModel()
            {
                Account = request.Account,
                Password = AuthManager.Instance.GetPassword(),
                AccountType = AccountType.User,
            };

            var created = await _authRepository.CreateAuth(authModel, DateTime.Now.Ticks);

            if (created == false)
            {
                return MakeCommonErrorMessage("failed to create auth");
            }

            created = await _userAssetRepository.InsertUserAssetAsync(request.Account, DateTime.Now.Ticks);
            if (created == false)
            {
                return MakeCommonErrorMessage("failed to create user asset");
            }

            return new CreateAccountResponse()
            {
                Ok = true,
                Password = authModel.Password,
            };
        }
    }
}
