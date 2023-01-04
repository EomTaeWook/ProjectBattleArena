using DataContainer.Generated;
using System.Collections.Generic;

namespace GameContents
{
    public class UnitInfo
    {
        public string CharacterName { get; set; }

        public CharacterTemplate CharacterTemplate { get; set; }
        public int Level { get; set; }

        public int Atk { get; set; }

        public int Con { get; set; }

        public int Dex { get; set; }

        public List<SkillsTemplate> EquippedSkillDatas { get; set; } = new List<SkillsTemplate>();
    }
}
