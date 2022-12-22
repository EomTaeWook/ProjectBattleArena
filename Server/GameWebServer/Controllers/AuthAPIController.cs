using GameWebServer.Manager;
using GameWebServer.Models;
using Kosher.Log;
using Protocol.GameWebServerAndClient;
using ShareLogic;
using System.Text.Json;

namespace GameWebServer.Controllers
{
    public abstract class AuthAPIController<T> : APIController<T> where T : AuthRequest
    {
        private AuthTokenModel ValidateToken(string token)
        {
            try
            {
                var json = Cryptogram.Decrypt(token);
                return JsonSerializer.Deserialize<AuthTokenModel>(json);
            }
            catch(Exception ex)
            {
                LogHelper.Error(ex);
                return null;
            }
        }
        public override async Task<IGWCResponse> Process(T request)
        {
            if(string.IsNullOrEmpty(request.Token) == true)
            {
                return new ErrorResponse()
                {
                    ErrorMessage = "token is empty"
                };
            }
            var authModel = ValidateToken(request.Token);

            if (authModel == null)
            {
                return MakeCommonErrorMessage("token broken");
            }

            var response = await Process(authModel.Account, request);

            if(response is ErrorResponse == false)
            {
                await UserLogManager.Instance.InsertLogAsync(authModel.Account, Request.Path, response);
            }
            return response;
        }
        public abstract Task<IGWCResponse> Process(string account, T request);
    }
}
