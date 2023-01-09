using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using TemplateContainers;

namespace DataContainer.Generated
{
    public partial class SkillEffectsAbnormalStatusTemplate : BaseTemplate
    {
        public DataContainer.AbnormalStatusType AbnormalStatusType { get; set; }
        public int Duration { get; set; }
    }
}
