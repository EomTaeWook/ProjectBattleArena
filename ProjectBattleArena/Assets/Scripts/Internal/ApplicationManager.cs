using Assets.Scripts.Models;
using Kosher.Framework;
using Newtonsoft.Json;
using Protocol.GameWebServerAndClient;
using Protocol.GameWebServerAndClient.ShareModels;
using ShareLogic;
using System;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Internal
{
    internal class ApplicationManager : Singleton<ApplicationManager>
    {
        private const string LocalUrl = @"http://localhost:30000";
        private const string DevUrl = @"http://13.125.232.85:30000";

        public string CurrentServerUrl { get; private set; }
        private Vector2Int _resolution = new Vector2Int(768, 1280);
        private int reqeustCount;
        public ServerType ServerType { get; private set; }
        public void Init(ServerType serverType)
        {
            SetResolution(_resolution.x, _resolution.y);

            switch(serverType)
            {
                case ServerType.Local:
                    CurrentServerUrl = LocalUrl;
                    break;
                case ServerType.Dev:
                    CurrentServerUrl = DevUrl;
                    break;
                default:
                    throw new InvalidOperationException($"invalid server type : {serverType}");
            }
            ServerType = serverType;
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
            return GetUserToken(MakeUserToken());
        }
        public string GetUserToken(TokenData tokenData)
        {
            var json = JsonConvert.SerializeObject(tokenData);
            var encrypt = Cryptogram.Encrypt(json);
            return encrypt;
        }
        public TokenData MakeUserToken()
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
                Count = reqeustCount++,
                CharacterName = CharacterManager.Instance.CharacterName,
            };

            return tokenModel;
        }
    }
}
