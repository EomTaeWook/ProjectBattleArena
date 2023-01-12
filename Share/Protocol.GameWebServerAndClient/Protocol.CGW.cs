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

        public int CharacterTemplateId { get; set; }
    }
    public class EquipmentSkill : AuthRequest
    {
        public string CharacterName { get; set; }
        public int SkillTemplateId { get; set; }
        public int SlotIndex { get; set; }
    }
    public class PurchaseGoods : AuthRequest
    {
        public string CharacterName { get; set; }
        public int TemplateId { get; set; }
    }
    public class PurchaseGoodsByAdmin : AuthRequest
    {
        public string CharacterName { get; set; }
        public int TemplateId { get; set; }
    }

}
