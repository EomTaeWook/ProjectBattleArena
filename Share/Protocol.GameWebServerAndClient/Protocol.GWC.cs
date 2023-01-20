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

    public class MountingSkillResponse : ServerResponse
    {
        public int SlotIndex { get; set; }

        public long ChangedSkillId { get; set; }
    }
    public class PurchaseGoodsResponse : ServerResponse
    {
        public RewardData RewardDiff { get; set; }
    }
    public class PurchaseGoodsByAdminResponse : ServerResponse
    {
        public RewardData RewardDiff { get; set; }
    }
    public class ChallengeArenaResponse : ServerResponse
    {
        public CharacterData OpponentCharacterData { get; set; }
        public bool BattleWin { get; set; }
        public int RandomSeed { get; set; }
        public int ArenaTicketRemainChargeTime { get; set; }
        public RewardData RewardData { get; set; }
        public int AreanTicketDiff { get; set; }
    }
}
