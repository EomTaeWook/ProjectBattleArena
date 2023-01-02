using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using TemplateContainers;

namespace DataContainer.Generated
{
    public partial class SkillsTemplate : BaseTemplate
    {
        public sealed partial class InnerEffets
        {
            public DataContainer.TargetType TargetType { get; set; }
            public string Effect { get; set; }
            public SkillEffectsTemplate EffectRef { get; set; }
        }
        public int Range { get; set; }
        public int NeedCost { get; set; }
        public List<InnerEffets> Effets { get; set; } = new List<InnerEffets>();
        public override void MakeRefTemplate()
        {
            foreach(var item in Effets)
            {
                item.EffectRef = TemplateContainer<SkillEffectsTemplate>.Find(item.Effect);
                if (item.EffectRef.Invalid() == true)
                {
                    Debug.Assert(false, $"SkillsTemplate Ref Data not found! Ref Field : {item.Effect}");
                }
            }
        }
    }
}
