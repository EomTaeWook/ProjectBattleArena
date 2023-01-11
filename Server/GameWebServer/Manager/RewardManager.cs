using DataContainer.Generated;
using Kosher.Framework;
using TemplateContainers;

namespace GameWebServer.Manager
{
    public class RewardManager : Singleton<RewardManager>
    {
        public RewardManager()
        {
            foreach(var template in TemplateContainer<SkillsTemplate>.Values)
            {

            }
        }
    }
}
