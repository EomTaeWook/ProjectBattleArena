using Assets.Scripts.Internal;
using Protocol.GameWebServerAndClient;
using System.Threading.Tasks;

public class TitleScene : BaseScene
{
    public override void OnAwakeScene()
    {
        ApplicationManager.Instance.SetResolution(1080, 1920);

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
            SceneManager.Instance.LoadScene(Assets.Scripts.Models.SceneType.LobbyScene);
        }
        else
        {

        }
    }
}
