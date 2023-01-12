using BA.Models;
using BA.Repository;
using BA.Repository.Helper;
using DataContainer;
using Kosher.Framework;
using Kosher.Log;
using Protocol.GameWebServerAndClient.ShareModels;

namespace GameWebServer.Manager
{
    public class AssetManager : Singleton<AssetManager>
    {
        public async Task<Tuple<long>> LoadAssetAsync(string account, AssetType assetType)
        {
            var userAssetRepository = DBServiceHelper.GetService<UserAssetRepository>();

            var loadAsset = await userAssetRepository.LoadUserAssetAsync(account);

            if(loadAsset == null)
            {
                return null;
            }

            if(assetType == AssetType.Cash)
            {
                return Tuple.Create((long)loadAsset.Cash);
            }
            else if(assetType == AssetType.GachaSkill)
            {
                return Tuple.Create((long)loadAsset.GachaSkill);
            }
            else
            {
                LogHelper.Fatal($"invalid asset type : {assetType}");
                return Tuple.Create(0L);
            }

        }
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
        public async Task<bool> ModifyGachaSkillAsync(string account, int currentValue, int updateValue, ChangedAssetReason reason)
        {
            var userAssetRepository = DBServiceHelper.GetService<UserAssetRepository>();

            var updated = await userAssetRepository.ModifyGachaSkill(account, currentValue, updateValue);

            if (updated == false)
            {
                return false;
            }

            await UserLogManager.Instance.InsertLogAsync(account, "GachaSkill", new ChangedAssetLogModel()
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
        public async Task<bool> ModifyAssetAsync(string account,
            AssetType assetType,
            long currentValue,
            long updateValue,
            ChangedAssetReason reason)
        {
            var result = false;
            if(assetType == AssetType.Cash)
            {
                result = await ModifyCashAsync(account, (int)currentValue, (int)updateValue, reason);
            }
            else if(assetType == AssetType.GachaSkill)
            {
                result = await ModifyGachaSkillAsync(account, (int)currentValue, (int)updateValue, reason);
            }
            else
            {

            }
            return result;
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
