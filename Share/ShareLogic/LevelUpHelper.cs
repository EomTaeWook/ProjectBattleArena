using DataContainer.Generated;
using TemplateContainers;

namespace ShareLogic
{
    public class LevelUpHelper
    {
        public LevelUpHelper()
        {
        }
        public static int GetLevel(int exp)
        {
            var currentLevel = 0;
            foreach (var item in TemplateContainer<LevelUpTemplate>.Values)
            {
                if(exp < item.NeedExp)
                {
                    return currentLevel;
                }
                currentLevel++;
            }
            return 1;
        }
    }
}
