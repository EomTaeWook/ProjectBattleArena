using Kosher.Framework;
using Protocol.GameWebServerAndClient;
using Protocol.GameWebServerAndClient.ShareModels;
using ShareLogic;
using System;
using System.Text;
using System.Threading.Tasks;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

namespace Assets.Scripts.Internal
{
    internal class ApplicationManager : Singleton<ApplicationManager>
    {
        private const string LocalUrl = @"http://localhost:10000";
        private const string DevUrl = @"http://localhost:10000";

        public string CurrentServerUrl { get; private set; }
        private Vector2Int _resolution = new Vector2Int(768, 1280);
        private int reqeustCount;
        public void Init()
        {
            SetResolution(_resolution.x, _resolution.y);

            CurrentServerUrl = LocalUrl;
        }
        private void SetResolution(int width, int height)
        {
            Screen.SetResolution(width, height, true);
        }
        public Vector2Int GetResolution()
        {
            return _resolution;
        }

        public async Task<bool> GetSecurityKeyAsync()
        {
            var response = await HttpRequestHelper.Request<GetSecurityKey, GetSecurityKeyResponse>(new GetSecurityKey());
            if (response.Ok == false)
            {
                UIManager.Instance.ShowAlert("알림", "초기화에 실패하였습니다.");
                return false;
            }

            Cryptogram.SetPublicKey(response.SecurityKey);
            return true;
        }
        
        public string GetUserToken()
        {
            var userData = PlayerManager.Instance.GetUserData();
            if (userData == null)
            {
                throw new ArgumentNullException("user data");
            }

            TokenData tokenModel = new TokenData()
            {
                Account = userData.Account,
                Password = userData.Password,
                Count = ++reqeustCount
            };
            var json = Newtonsoft.Json.JsonConvert.SerializeObject(tokenModel);

            var token = Cryptogram.Encrypt(json);

            return token;
        }
    }
}
