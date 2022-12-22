using Protocol.GameWebServerAndClient.ShareModels;

namespace Protocol.GameWebServerAndClient
{
    public interface ICGWRequest
    {
    }
    public class AuthRequest : ICGWRequest
    {
        public string Token { get; set; }
    }
    public class Login : AuthRequest
    {
    }
    
    public class CreateAccount : ICGWRequest
    {
        public string Account { get; set; }
    }
    public class GetSecurityKey : ICGWRequest
    { }

    public class CreateCharacter : AuthRequest
    {
        public string CharacterName { get; set; }

        public JobType Job { get; set; }
    }

}
