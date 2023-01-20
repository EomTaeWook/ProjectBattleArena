using System.Collections.Generic;
using TemplateContainers;

namespace DataContainer.Generated
{
    public partial class SkillsGroupTemplate : BaseTemplate
    {
        private readonly Dictionary<GradeType, SkillsTemplate> _skillsTemplateByGradeType = new Dictionary<GradeType, SkillsTemplate>();
        public SkillsTemplate RareRef { get; private set; }
        public SkillsTemplate EpicRef { get; private set; }
        public SkillsTemplate LegendaryRef { get; private set; }

        private SkillsTemplate _dummySkillTemplate = new SkillsTemplate();
        public override void Combine()
        {
            if(!string.IsNullOrEmpty(this.Rare))
            {
                RareRef = TemplateContainer<SkillsTemplate>.Find(this.Rare);
            }
            else
            {
                RareRef = _dummySkillTemplate;
            }

            if (!string.IsNullOrEmpty(this.Epic))
            {
                EpicRef = TemplateContainer<SkillsTemplate>.Find(this.Epic);
            }
            else
            {
                EpicRef = _dummySkillTemplate;
            }

            if (!string.IsNullOrEmpty(this.Legendary))
            {
                LegendaryRef = TemplateContainer<SkillsTemplate>.Find(this.Legendary);
            }
            else
            {
                LegendaryRef = _dummySkillTemplate;
            }
            _skillsTemplateByGradeType.Add(GradeType.Normal, NormalRef);
            _skillsTemplateByGradeType.Add(GradeType.Rare, RareRef);
            _skillsTemplateByGradeType.Add(GradeType.Epic, EpicRef);
            _skillsTemplateByGradeType.Add(GradeType.Legendary, LegendaryRef);
        }
        public SkillsTemplate this[GradeType gradeType]
        {
            get
            {
                _skillsTemplateByGradeType.TryGetValue(gradeType, out SkillsTemplate template);

                return template;
            }
        }

    }

}
