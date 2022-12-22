using GameWebServer.Models;
using Kosher.Log;
using Microsoft.AspNetCore.Mvc;
using Protocol.GameWebServerAndClient;
using System.Runtime.CompilerServices;

namespace GameWebServer.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public abstract class APIController<T> : ControllerBase where T : ICGWRequest
    {
        [HttpPost]
        public async Task<JsonResult> Post(T request)
        {
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
        public abstract Task<IGWCResponse> Process(T request);
    }
}
