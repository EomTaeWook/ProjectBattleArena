using Assets.Scripts.Internal;
using Assets.Scripts.Scenes;
using Kosher.Log;
using Kosher.Log.LogTarget;
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
        var logConfiguration = new LogConfiguration();
        var consoleLogTarget = new ConsoleLogTarget();
        var rule = new Kosher.Log.Model.Rule.LogRule("unity logger", LogLevel.Debug, consoleLogTarget);
        logConfiguration.AddRule("unity console", rule);
        logConfiguration.AddTarget("unity console", consoleLogTarget);
        LogBuilder.Configuration(logConfiguration);
        LogBuilder.Build();

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
