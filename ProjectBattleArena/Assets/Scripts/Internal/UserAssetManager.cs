using Kosher.Framework;
using Protocol.GameWebServerAndClient.ShareModels;

namespace Assets.Scripts.Internal
{
    public class UserAssetManager : Singleton<UserAssetManager>
    {
        private AssetData _assetData;

        public AssetData GetAssetData()
        {
            return _assetData;
        }

        public void Init(AssetData assetData)
        {
            _assetData = assetData;
        }
    }
}
