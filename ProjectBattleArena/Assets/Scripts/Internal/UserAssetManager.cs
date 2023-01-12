using Kosher.Framework;
using Protocol.GameWebServerAndClient.ShareModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Internal
{
    public class UserAssetManager : Singleton<UserAssetManager>
    {
        private AssetData _assetData;

        public void Init(AssetData assetData)
        {
            _assetData = assetData;
        }
        public void Update(RewardData rewardData)
        {
            _assetData.GachaSkill += rewardData.GachaSkillDiff;
            _assetData.Cash += rewardData.CashDiff;
        }
    }
}
