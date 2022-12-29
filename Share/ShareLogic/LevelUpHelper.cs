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
            foreach (var item in TemplateContainer<LevelUpTemplate>.Values)
            {
                if(exp < item.NeedExp)
                {
                    return item.Level;
                }
            }
            return 1;
        }
    }
}
