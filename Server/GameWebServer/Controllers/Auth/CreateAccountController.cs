using BA.Models;
using GameWebServer.Manager;
using Protocol.GameWebServerAndClient;
using Repository;

namespace GameWebServer.Controllers.Auth
{
    public class CreateAccountController : APIController<CreateAccount>
    {
        readonly AuthRepository _authRepository;
        public CreateAccountController(AuthRepository authRepository)
        {
            _authRepository = authRepository;
        }
        public override async Task<IGWCResponse> Process(CreateAccount request)
        {
            var loadModel = await _authRepository.LoadAuth(request.Account);

            if(loadModel == null)
            {
                return MakeErrorMessage("DB Error");
            }

            if(string.IsNullOrEmpty(loadModel.Account) == false)
            {
                return MakeErrorMessage("already create account");
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
                return MakeErrorMessage("failed to create auth");
            }

            return new CreateAccountResponse()
            {
                Ok = true,
                Password = authModel.Password
            };
        }
    }
}
