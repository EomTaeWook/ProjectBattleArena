using BA.Models;
using BA.Repository;
using BA.Repository.Helper;
using Kosher.Framework;
using Protocol.GameWebServerAndClient.ShareModels;

namespace GameWebServer.Manager
{
    public class AssetManager : Singleton<AssetManager>
    {
        public async Task<bool> ModifyGoldAsync(string account, long currentValue, long updateValue, ChangedAssetReason reason)
        {
            var userAssetRepository = DBServiceHelper.GetService<UserAssetRepository>();

            var updated = await userAssetRepository.ModifyGold(account, currentValue, updateValue);

            if(updated == false)
            {
                return false;
            }

            await UserLogManager.Instance.InsertLogAsync(account, "ModifyGold", new ChangedAssetLogModel()
            {
                CurrentValue = currentValue,
                UpdateValue = updateValue,
                Reason = reason
            });
            return true;
        }
        public async Task<bool> ModifyCashAsync(string account, int currentValue, int updateValue, ChangedAssetReason reason)
        {
            var userAssetRepository = DBServiceHelper.GetService<UserAssetRepository>();

            var updated = await userAssetRepository.ModifyCash(account, currentValue, updateValue);

            if (updated == false)
            {
                return false;
            }

            await UserLogManager.Instance.InsertLogAsync(account, "ModifyCash", new ChangedAssetLogModel()
            {
                CurrentValue = currentValue,
                UpdateValue = updateValue,
                Reason = reason
            });
            return true;
        }
        public async Task<bool> ModifyArenaTicketAsync(string account,
            int currentValue,
            int updateValue,
            long latestUpdateTime,
            ChangedAssetReason reason)
        {
            var userAssetRepository = DBServiceHelper.GetService<UserAssetRepository>();

            var updated = await userAssetRepository.ModifyArenaTicket(account, currentValue, updateValue, latestUpdateTime);

            if (updated == false)
            {
                return false;
            }

            await UserLogManager.Instance.InsertLogAsync(account, "ModifyArenaTicket", new ChangedAssetLogModel()
            {
                CurrentValue = currentValue,
                UpdateValue = updateValue,
                Reason = reason
            });
            return true;
        }
    }
}
