using Assets.Scripts.Internal;
using Assets.Scripts.Scenes;
using Protocol.GameWebServerAndClient;
using ShareLogic;
using System.Threading.Tasks;

public class TitleSceneController : SceneController<TitleSceneController>
{
    TitleScene scene;
    public override void BindScene(BaseScene baseScene)
    {
        scene = baseScene as TitleScene;
    }

    public void Init()
    {
        ApplicationManager.Instance.Init();

        PlayerManager.Instance.LoadUserData();
    }

    public async Task<bool> ReqeustSecurityKeyAsync()
    {
        var response = await HttpRequestHelper.Request<GetSecurityKey, GetSecurityKeyResponse>(new GetSecurityKey());
        if (response.Ok == false)
        {
            return false;
        }

        Cryptogram.SetPublicKey(response.SecurityKey);

        return true;
    }

    public async Task<bool> RequestLoginAsync()
    {
        var response = await HttpRequestHelper.Request<Login, LoginResponse>(new Login()
        {
            Token = ApplicationManager.Instance.GetUserToken()
        });

        if (response.Ok == false)
        {
            return true;
        }

        CharacterManager.Instance.Init(response.CharacterDatas);

        return true;

    }
}
