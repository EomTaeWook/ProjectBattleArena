using Assets.Scripts.Models;
using Kosher.Framework;
using Newtonsoft.Json;
using Protocol.GameWebServerAndClient;
using System;
using System.IO;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Internal
{
    internal class PlayerManager : Singleton<PlayerManager>
    {
        private UserData _userData;
        private const string UserFileName = "userData.json";
        private readonly string _path = Application.persistentDataPath;

        public PlayerManager()
        {
#if UNITY_EDITOR
            _path = "./";
#endif
        }

        public void LoadUserData()
        {
            var fullPath = Path.Combine(_path, UserFileName);
            if(File.Exists(fullPath) == true)
            {
                var json = File.ReadAllText(fullPath);

                _userData = JsonConvert.DeserializeObject<UserData>(json);
            }
        }
        public void SaveUserData()
        {
            var json = JsonConvert.SerializeObject(_userData);

            var fullPath = Path.Combine(_path, UserFileName);
            File.WriteAllText(fullPath, json);
        }
        public async Task<bool> MakeAccountAsync()
        {
            var account = $"{SystemInfo.deviceUniqueIdentifier}{DateTime.Now.Ticks}";

            var response = await HttpRequestHelper.Request<CreateAccount, CreateAccountResponse>(new CreateAccount()
            {
                Account = account
            });

            if(response.Ok == false)
            {
                return false;
            }

            _userData = new UserData()
            {
                Account = account,
                Password = response.Password
            };

            SaveUserData();

            return true;
        }
        public bool IsExists()
        {
            return _userData != null;
        }

        public UserData GetUserData()
        {
            return _userData;
        }
    }
}
