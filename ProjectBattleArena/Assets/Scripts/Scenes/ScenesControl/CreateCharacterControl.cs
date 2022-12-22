using Assets.Scripts.Internal;
using Kosher.Framework;
using Protocol.GameWebServerAndClient;
using Protocol.GameWebServerAndClient.ShareModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Scenes.ScenesControl
{
    internal class CreateCharacterControl : Singleton<CreateCharacterControl>
    {
        public JobType CurrentJobType { get; private set; } = JobType.SwordMan;
        public void ChangeCurrentCharacter(JobType jobType)
        {
            CurrentJobType = jobType;
        }
        public void Dispose()
        {
            CurrentJobType = JobType.SwordMan;
        }
        public async Task RequestCreateCharacterAsync(string characterName)
        {
            if (string.IsNullOrEmpty(characterName) == true)
            {
                var alert = AlertPopup.Instantiate();
                alert.SetContent("알림", "캐릭터명을 입력해주세요.");
                UIManager.Instance.AddPopupUI(alert);
                return;
            }

            var request = new CreateCharacter()
            {
                CharacterName = characterName,
                Job = CurrentJobType
            };
            var response = await HttpRequestHelper.AuthRequest<CreateCharacter, CreateCharacterResponse>(request);

            if(response.Ok == false)
            {
                //UIManager.Instance.AddPopupUI();
                return;
            }

            SceneManager.Instance.LoadScene(Models.SceneType.LobbyScene);
        }

    }
}
