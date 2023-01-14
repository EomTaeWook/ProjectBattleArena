using BA.Models;
using BA.Repository;
using BA.Repository.Helper;
using Kosher.Framework;
using Protocol.GameWebServerAndClient.ShareModels;

namespace GameWebServer.Manager
{
    public class RewardGiverManager : Singleton<RewardGiverManager>
    {
        public async Task<bool> GiveRewradsAsync(string account,
            string characterName,
            RewardData rewardData,
            GiveRewardReason reason)
        {
            var skillRepository = DBServiceHelper.Instance.GetService<SkillRepository>();
            var currentTime = DateTime.Now.Ticks;
            foreach (var item in rewardData.AcquiredSkills)
            {
                var inserted = await skillRepository.InsertSkill(characterName, item.TemplateId, currentTime);

                if (inserted == -1)
                {
                    return false;
                }

                item.Id = inserted;
            }
            var userAssetRepository = DBServiceHelper.Instance.GetService<UserAssetRepository>();

            var loadUserAsset = await userAssetRepository.LoadUserAssetAsync(account);
            if(loadUserAsset == null)
            {
                return false;
            }

            if (rewardData.CashDiff > 0)
            {
                var modify = await userAssetRepository.ModifyCash(account,
                    loadUserAsset.Cash,
                    loadUserAsset.Cash + rewardData.CashDiff);

                if(modify == false)
                {
                    return false;
                }
            }
            if(rewardData.GachaSkillDiff > 0)
            {
                var modify = await userAssetRepository.ModifyCash(account,
                    loadUserAsset.GachaSkill,
                    loadUserAsset.GachaSkill + rewardData.GachaSkillDiff);

                if (modify == false)
                {
                    return false;
                }
            }

            GiveRewardLog log = new GiveRewardLog()
            {
                GiveRewardReason = reason,
                RewardData = rewardData
            };

            await UserLogManager.Instance.InsertLogAsync<GiveRewardLog>(account, "GiveReward", log);

            return true;
        }
    }
}
