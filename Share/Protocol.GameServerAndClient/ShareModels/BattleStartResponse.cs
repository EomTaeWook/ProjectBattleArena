using Protocol.GameWebServerAndClient.ShareModels;
using System.Collections.Generic;

namespace Protocol.GSC.ShareModels
{
    public class BattleStartResponse
    {
        public bool Ok { get; set; }

        public string Reason { get; set; }
        public int RandomSeed { get; set; }

        public List<CharacterData> Allies { get; set; } = new List<CharacterData>();

        public List<CharacterData> Enemies { get; set; } = new List<CharacterData>();
    }
}
