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
            return new EmptyResult();
        }
    }
}
