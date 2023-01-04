using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using TemplateContainers;

namespace DataContainer.Generated
{
    public partial class SkillEffectsTemplate : BaseTemplate
    {
        public DataContainer.TargetType TargetType { get; set; }
        public DataContainer.EffectType EffectType { get; set; }
        public string EffectPart { get; set; }
    }
}
