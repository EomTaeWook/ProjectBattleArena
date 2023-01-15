using GameWebServer.Manager;
using GameWebServer.Models;
using Kosher.Log;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using Protocol.GameWebServerAndClient;
using Protocol.GameWebServerAndClient.ShareModels;
using ShareLogic;
using System.Runtime.CompilerServices;
using System.Text.Json;

namespace GameWebServer.Controllers
{
    [Route("[controller]")]
    public abstract class APIController<T> : ControllerBase where T : ICGWRequest
    {
        [HttpPost]
        public async Task<JsonResult> Post([FromBody]T request)
        {
            if(ServiceManager.Instance.IsServerOn() == false)
            {
                return new JsonResult(new ServerResponse()
                {
                    Maintenance = true,
                    Ok = false
                });
            }
            LogHelper.Info($"Thread : {Environment.CurrentManagedThreadId} | {HttpContext.Request.Path}");
            var response = await Process(request);
            return new JsonResult(response);
        }

        public ErrorResponse MakeCommonErrorMessage(string message, [CallerFilePath] string fileName = "", [CallerLineNumber] int fileNumber = 0)
        {
            LogHelper.Error($"message : {message}", fileName, fileNumber);
            return new ErrorResponse()
            {
                ErrorMessage = message,
                Ok = false
            };
        }

        public ErrorResponse MakeErrorMessage(string account, string message, [CallerFilePath]string fileName= "", [CallerLineNumber]int fileNumber = 0)
        {
            LogHelper.Error($"user : {account} message : {message}", fileName, fileNumber);
            return new ErrorResponse()
            {
                ErrorMessage = message,
                Ok = false
            };
        }
        protected TokenData GetTokenData(string token)
        {
            try
            {
                var json = Cryptogram.Decrypt(token);
                return JsonSerializer.Deserialize<TokenData>(json);
            }
            catch (Exception ex)
            {
                LogHelper.Error(ex);
                return null;
            }
        }
        protected TokenData ValidateToken(string token)
        {
            try
            {
                var json = Cryptogram.Decrypt(token);
                var data = JsonSerializer.Deserialize<TokenData>(json);

                if (string.IsNullOrEmpty(data.Account) == true)
                {
                    return null;
                }
                else if(string.IsNullOrEmpty(data.Password) == true)
                {
                    return null;
                }
                else if(string.IsNullOrEmpty(data.CharacterName) == true)
                {
                    return null;
                }
                return data;
            }
            catch (Exception ex)
            {
                LogHelper.Error(ex);
                return null;
            }
        }
        public abstract Task<IGWCResponse> Process(T request);
    }
}
