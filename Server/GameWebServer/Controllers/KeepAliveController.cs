using Kosher.Log;
using Microsoft.AspNetCore.Mvc;

namespace GameWebServer.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class KeepAliveController : ControllerBase
    {
        [HttpPost]
        public EmptyResult Post()
        {
            LogHelper.Debug($"Thread : {Environment.CurrentManagedThreadId} | {HttpContext.Request.Path}");

            return new EmptyResult();
        }
        [HttpGet]
        public EmptyResult Get()
        {
            LogHelper.Debug($"Thread : {Environment.CurrentManagedThreadId} | {HttpContext.Request.Path}");

            return new EmptyResult();
        }
    }
}
