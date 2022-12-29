using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using TemplateContainers;

namespace DataContainer.Generated
{
    public partial class SkillsTemplate : BaseTemplate
    {
        public string StringId { get; set; }
        public DataContainer.JobType JobType { get; set; }
        public int Range { get; set; }
        public int Cost { get; set; }
    }
}
