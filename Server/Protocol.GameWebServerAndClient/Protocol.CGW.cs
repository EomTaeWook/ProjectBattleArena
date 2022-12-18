using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Protocol.GameWebServerAndClient
{
    public interface ICGWRequest
    {
    }

    public class AuthRequest : ICGWRequest
    {
        public string Account { get; set; }
        public string Password { get; set; }
    }
    public class Login : AuthRequest
    {

    }
    public class CreateAccount : ICGWRequest
    {
        public string Account { get; set; }
    }
    
}
