using Protocol.GameWebServerAndClient.ShareModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Protocol.GameWebServerAndClient
{
    public interface IGWCResponse
    {
        bool Ok { get; set; }
    }

    public class LoginResponse : IGWCResponse
	{
        public List<CharacterData> CharacterDatas { get; set; } = new List<CharacterData>();
        public bool Ok { get; set; }
    }
    public class CreateAccountResponse : IGWCResponse
    {
        public bool Ok { get; set; }

        public string Password { get; set; }
    }

}
