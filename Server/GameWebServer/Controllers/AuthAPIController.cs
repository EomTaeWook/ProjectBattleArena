using GameWebServer.Manager;
using GameWebServer.Models;
using Protocol.GameWebServerAndClient;

namespace GameWebServer.Controllers
{
    public abstract class AuthAPIController<T> : APIController<T> where T : AuthRequest
    {
        public override async Task<IGWCResponse> Process(T request)
        {
            if(string.IsNullOrEmpty(request.Account) == true)
            {
                return MakeErrorMessage("account is empty");
            }
            else if(string.IsNullOrEmpty(request.Password) == true)
            {
                return MakeErrorMessage("password is empty");
            }
            var response = await Process(request.Account, request);

            if(response is ErrorResponse == false)
            {
                await ActionLogManager.Instance.InsertLogAsync(request.Account, Request.Path, response);
            }            

            return response;
        }
        public abstract Task<IGWCResponse> Process(string account, T request);
    }
}
