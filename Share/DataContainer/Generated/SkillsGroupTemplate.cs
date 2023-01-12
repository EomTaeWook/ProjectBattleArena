using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using TemplateContainers;

namespace DataContainer.Generated
{
    public partial class SkillsGroupTemplate : BaseTemplate
    {
        public bool IsBaseAttack { get; set; }
        public int NeedCost { get; set; }
        public int Range { get; set; }
        public bool IsCasting { get; set; }
        public int CastingTime { get; set; }
        public string UseCharacter { get; set; }
        public CharacterTemplate UseCharacterRef { get; set; }
        public string Normal { get; set; }
        public SkillsTemplate NormalRef { get; set; }
        public string Rare { get; set; }
        public string Epic { get; set; }
        public string Legendary { get; set; }
        public override void MakeRefTemplate()
        {
            UseCharacterRef = TemplateContainer<CharacterTemplate>.Find(UseCharacter);
            if(UseCharacterRef.Invalid() == true)
            {
                Debug.Assert(false, $"SkillsGroupTemplate Ref Data not found! Ref Field : {UseCharacter}");
            }

            NormalRef = TemplateContainer<SkillsTemplate>.Find(Normal);
            if(NormalRef.Invalid() == true)
            {
                Debug.Assert(false, $"SkillsGroupTemplate Ref Data not found! Ref Field : {Normal}");
            }

        }
    }
}
