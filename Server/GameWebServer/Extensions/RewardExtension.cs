using DataContainer;
using Protocol.GameWebServerAndClient.ShareModels;

namespace GameWebServer.Extensions
{
    public static class RewardExtension
    {
        public static void ModifyAsset(this RewardData reward, AssetType assetType, int diff)
        {
            if (assetType == AssetType.Cash)
            {
                reward.CashDiff += diff;
            }
            else if (assetType == AssetType.GachaSkill)
            {
                reward.GachaSkillDiff += diff;
            }
            else
            {
            }
        }
    }
}
