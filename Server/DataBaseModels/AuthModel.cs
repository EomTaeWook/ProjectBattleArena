using Protocol.GameWebServerAndClient.ShareModels;

namespace BA.Models
{
    public class AuthModel
    {
        public string Account { get; set; }

        public string Password { get; set; }

        public AccountType AccountType { get; set; }
    }
}
