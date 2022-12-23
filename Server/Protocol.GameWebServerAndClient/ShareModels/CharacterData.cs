namespace Protocol.GameWebServerAndClient.ShareModels
{
    public class CharacterData
    {
        public string CharacterName { get; set; }

        public JobType Job { get; set; }

        public int Exp { get; set; }

        public int Str { get; set; }

        public int Wiz { get; set; }

        public int Con { get; set; }

        public int Dex { get; set; }

        public int StatPoint { get; set; }

        public long CreateTime { get; set; }
    }
}
