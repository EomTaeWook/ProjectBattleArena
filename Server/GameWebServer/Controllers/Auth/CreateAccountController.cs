using BA.Models;
using GameWebServer.Manager;
using GameWebServer.Models;
using Protocol.GameWebServerAndClient;
using Protocol.GameWebServerAndClient.ShareModels;
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
                return new ErrorResponse 
                { 
                    ErrorMessage = "DB Error"
                };
            }

            if(string.IsNullOrEmpty(loadModel.Account) == false)
            {
                return new ErrorResponse
                {
                    ErrorMessage = "already create account"
                };
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
                return new ErrorResponse
                {
                    ErrorMessage = "failed to create auth"
                };
            }

            return new CreateAccountResponse()
            {
                Ok = true,
                Password = authModel.Password
            };
        }
    }
}
