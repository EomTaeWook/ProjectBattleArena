using Assets.Scripts.Internal;
using Protocol.GameWebServerAndClient;
using System.Threading.Tasks;

public class TitleScene : BaseScene
{
    public override void OnAwakeScene()
    {
        ApplicationManager.Instance.Init();

        PlayerManager.Instance.LoadUserData();
    }

    public override void OnDestroyScene()
    {
    }

    public async void OnStartButtonClickAsync()
    {
        var init = await ApplicationManager.Instance.GetSecurityKeyAsync();
        if (init == false)
        {
            return;
        }

        if (PlayerManager.Instance.IsExists() == false)
        {
            var maked = await PlayerManager.Instance.MakeAccountAsync();
            if (maked == false)
            {
                return;
            }
        }
        var response = await HttpRequestHelper.Request<Login, LoginResponse>(new Login()
        {
            Token = ApplicationManager.Instance.GetUserToken()
        });
        if (response.Ok == true)
        {
            CharacterManager.Instance.Init(response.CharacterDatas);

            if (CharacterManager.Instance.CharacterCount > 0)
            {
                SceneManager.Instance.LoadScene(Assets.Scripts.Models.SceneType.LobbyScene);
            }
            else
            {
                SceneManager.Instance.LoadScene(Assets.Scripts.Models.SceneType.CreateCharacterScene);
            }
        }
        else
        {
            UIManager.Instance.ShowAlert("알림", "로그인에 실패하였습니다.");
        }
    }
}
