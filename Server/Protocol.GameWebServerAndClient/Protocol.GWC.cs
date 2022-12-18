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
    public class ServerResponse : IGWCResponse
    {
        public bool Maintenance { get; set; }
        public bool Ok { get; set; }
    }

    public class LoginResponse : ServerResponse
    {
        public List<CharacterData> CharacterDatas { get; set; } = new List<CharacterData>();
    }
    public class CreateAccountResponse : ServerResponse
    {
        public string Password { get; set; }
    }

    public class CreateCharacterResponse : ServerResponse
    {
        public CharacterData NewCharacterData { get; set; }
    }


}
