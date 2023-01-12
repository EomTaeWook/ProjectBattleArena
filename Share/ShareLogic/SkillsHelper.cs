using DataContainer.Generated;
using Kosher.Framework;
using System.Collections.Generic;
using TemplateContainers;

namespace ShareLogic
{
    public class SkillsHelper : Singleton<SkillsHelper>
    {
        readonly Dictionary<int, SkillsGroupTemplate> _skillToParentMap = new Dictionary<int, SkillsGroupTemplate>();
        public SkillsHelper()
        {
            foreach(var item in TemplateContainer<SkillsGroupTemplate>.Values)
            {
                _skillToParentMap.Add(item.NormalRef.Id, item);

                if(item.RareRef.Invalid() == false)
                {
                    _skillToParentMap.Add(item.RareRef.Id, item);
                }
                else if(item.EpicRef.Invalid() == false)
                {
                    _skillToParentMap.Add(item.EpicRef.Id, item);
                }
                else if (item.LegendaryRef.Invalid() == false)
                {
                    _skillToParentMap.Add(item.LegendaryRef.Id, item);
                }
            }
        }
        public SkillsGroupTemplate GetSkillsGroupTemplate(int id)
        {
            return _skillToParentMap[id];
        }
    }
}
