using System.Collections.Generic;

namespace Protocol.GameWebServerAndClient.ShareModels
{
    public class CharacterData
    {
        public string CharacterName { get; set; }

        public int TemplateId { get; set; }

        public int Exp { get; set; }

        public int Atk { get; set; }

        public int Con { get; set; }

        public int Dex { get; set; }

        public int StatPoint { get; set; }

        public long CreateTime { get; set; }

        public List<long> MountingSkillDatas { get; set; } = new List<long>();

        public List<SkillData> SkillDatas { get; set; } = new List<SkillData>();
    }
}
