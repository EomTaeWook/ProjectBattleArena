using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using TemplateContainers;

namespace DataContainer.Generated
{
    public partial class SkillsTemplate : BaseTemplate
    {
        public int NeedCost { get; set; }
        public List<string> Effect { get; set; } = new List<string>();
        public List<SkillEffectsTemplate> EffectRef { get; set; } = new List<SkillEffectsTemplate>();
        public int Range { get; set; }
        public bool IsCasting { get; set; }
        public int CastingTime { get; set; }
        public override void MakeRefTemplate()
        {
            foreach(var EffectItem in Effect)
            {
                var template = TemplateContainer<SkillEffectsTemplate>.Find(EffectItem);
                if(template.Invalid() == true)
                {
                    Debug.Assert(false, $"SkillsTemplate Ref Data not found! Ref Field : {EffectItem}");
                }
                EffectRef.Add(template);
            }
        }
    }
}
