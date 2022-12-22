using Protocol.GameWebServerAndClient;

namespace GameWebServer.Models
{
    public class ErrorResponse : IGWCResponse
    {
        public string ErrorMessage { get; set; }
        public bool Ok { get; set; }
    }
}