using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using TemplateContainers;

namespace DataContainer.Generated
{
    public partial class CharacterTemplate : BaseTemplate
    {
        public sealed partial class InnerStat
        {
            public int HP { get; set; }
            public int Attack { get; set; }
            public int Defense { get; set; }
            public int AttackSpeed { get; set; }
            public int Critical { get; set; }
            public int Patience { get; set; }
            public int Block { get; set; }
            public int Hit { get; set; }
            public int Dodge { get; set; }
            public int Destruction { get; set; }
        }
        public InnerStat Stat { get; set; }
        public string BaseAttackSkill { get; set; }
        public SkillsTemplate BaseAttackSkillRef { get; set; }
        public override void MakeRefTemplate()
        {
            BaseAttackSkillRef = TemplateContainer<SkillsTemplate>.Find(BaseAttackSkill);
            if(BaseAttackSkillRef.Invalid() == true)
            {
                Debug.Assert(false, $"CharacterTemplate Ref Data not found! Ref Field : {BaseAttackSkill}");
            }
        }
    }
}
