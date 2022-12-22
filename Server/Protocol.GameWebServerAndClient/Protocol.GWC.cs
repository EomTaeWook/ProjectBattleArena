using Protocol.GameWebServerAndClient.ShareModel;
using System.Collections.Generic;

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

    public class GetSecurityKeyResponse : IGWCResponse
    {
        public bool Ok { get; set; }
        public string SecurityKey { get; set; }
    }
}
