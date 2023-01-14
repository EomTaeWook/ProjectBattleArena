using Kosher.Framework;
using Protocol.GameWebServerAndClient.ShareModels;

namespace Assets.Scripts.Internal
{
    public class RewardManager : Singleton<RewardManager>
    {
        public void Update(RewardData rewardData)
        {
            var assetData = UserAssetManager.Instance.GetAssetData();
            assetData.Cash += rewardData.CashDiff;
            assetData.GachaSkill += rewardData.GachaSkillDiff;
            CharacterManager.Instance.AddSkillDatas(rewardData.AcquiredSkills);
        }
    }
}
