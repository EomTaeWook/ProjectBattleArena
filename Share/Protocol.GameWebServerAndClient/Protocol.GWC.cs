using Protocol.GameWebServerAndClient.ShareModels;
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

        public AssetData AssetData { get; set; }
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

    public class EquipmentSkillResponse : ServerResponse
    {
    }
    public class PurchaseGoodsResponse : ServerResponse
    {
        public RewardData RewardDiff { get; set; }
    }
    public class PurchaseGoodsByAdminResponse : ServerResponse
    {
        public RewardData RewardDiff { get; set; }
    }
}
