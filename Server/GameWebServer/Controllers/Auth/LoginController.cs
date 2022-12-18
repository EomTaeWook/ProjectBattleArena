using Protocol.GameWebServerAndClient;
using Repository;

namespace GameWebServer.Controllers.Auth
{
    public class LoginController : APIController<Login>
    {
        private AuthRepository _authRepository;
        public LoginController(AuthRepository authRepository)
        {
            _authRepository = authRepository;
        }
        public override async Task<IGWCResponse> Process(Login request)
        {
            if(string.IsNullOrEmpty(request.Account) == true)
            {
                return MakeErrorMessage("account is empty");
            }
            if (string.IsNullOrEmpty(request.Password) == true)
            {
                return MakeErrorMessage("password is empty");
            }

            var loadModel = await _authRepository.LoadAuth(request.Account);

            if (loadModel == null)
            {
                return MakeErrorMessage("not found account");
            }
            
            if(loadModel.Password.Equals(request.Password) == false)
            {
                return MakeErrorMessage($"failed to login {request.Account} invalid password");
            }

            return new LoginResponse()
            {
                Ok = true,
                
            };
        }
    }
}
