using GameWebServer.Manager;
using GameWebServer.Models;
using Protocol.GameWebServerAndClient;
using Protocol.GameWebServerAndClient.ShareModels;

namespace GameWebServer.Controllers
{
    public abstract class AuthAPIController<T> : APIController<T> where T : AuthRequest
    {
        public override async Task<IGWCResponse> Process(T request)
        {
            if(string.IsNullOrEmpty(request.Token) == true)
            {
                return new ErrorResponse()
                {
                    ErrorMessage = "token is empty"
                };
            }
            var tokenModel = ValidateToken(request.Token);

            if (tokenModel == null)
            {
                return MakeCommonErrorMessage("token broken");
            }

            var response = await Process(tokenModel, request);

            if(response is ErrorResponse == false)
            {
                await UserLogManager.Instance.InsertLogAsync(tokenModel.Account, Request.Path, response);
            }
            return response;
        }
        public abstract Task<IGWCResponse> Process(TokenData tokenData, T request);
    }
}
