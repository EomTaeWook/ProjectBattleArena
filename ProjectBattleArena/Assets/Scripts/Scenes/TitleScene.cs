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
            Account = PlayerManager.Instance.GetUserData().Account,
            Password = PlayerManager.Instance.GetUserData().Password
        });

        if (response.Ok == true)
        {
            if (response.CharacterDatas.Count > 0)
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

        }
    }
}
