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
        public string Account { get; set; }
    }
    
    public class CreateAccount : ICGWRequest
    {
        public string Account { get; set; }
    }
    public class GetSecurityKey : ICGWRequest
    { }

    public class CreateCharacter : AuthRequest
    {
        public int CharacterTemplateId { get; set; }
    }
    public class MountingSkill : AuthRequest
    {
        public long SkillId { get; set; }
        public int SlotIndex { get; set; }
    }
    public class PurchaseGoods : AuthRequest
    {
        public int TemplateId { get; set; }
    }
    public class PurchaseGoodsByAdmin : AuthRequest
    {
        public int TemplateId { get; set; }
    }
    public class ChallengeArena : AuthRequest
    {
        public string OpponentId { get; set; }
    }
}
