namespace GameWebServer.Models
{
    public class AuthTokenModel
    {
        public string Account { get; set; }

        public string Password { get; set; }

        public long RefreshTime { get; set; }
    }
}
