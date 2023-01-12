using Microsoft.SqlServer.Server;
using System.Collections.Generic;
using System.Diagnostics;
using TemplateContainers;

namespace DataContainer.Generated
{
    public partial class SkillsTemplate : BaseTemplate
    {
        public List<SkillEffectsTemplate> EffectRef { get; private set; } = new List<SkillEffectsTemplate>();

        public override void Combine()
        {
            foreach(var item in Effect)
            {
                if(!string.IsNullOrEmpty(item))
                {
                    var template = TemplateContainer<SkillEffectsTemplate>.Find(item);

                    if(template.Invalid())
                    {
                        throw new System.Exception($"SkillEffectsTemplate Effect Ref Data not found! Ref Field : {item}");
                    }

                    EffectRef.Add(template);
                }
            }
        }
    }
}
