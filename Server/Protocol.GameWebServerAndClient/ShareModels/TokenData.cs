namespace Protocol.GameWebServerAndClient.ShareModels
{
    public class TokenData
    {
        public string Account { get; set; }

        public string Password { get; set; }

        public long RefreshTime { get; set; }
    }
}
