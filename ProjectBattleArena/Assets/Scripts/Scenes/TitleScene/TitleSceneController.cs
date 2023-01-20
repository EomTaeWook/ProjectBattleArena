using Assets.Scripts.Internal;
using Assets.Scripts.Models;
using Assets.Scripts.Scenes;
using DataContainer.Generated;
using Kosher.Log;
using Protocol.GameWebServerAndClient;
using ShareLogic;
using System.IO;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;

public class TitleSceneController : SceneController<TitleSceneController>
{
    TitleScene scene;
    
    public TitleSceneController()
    {
        var logConfiguration = new LogConfiguration();
        var logger = new UnityLogTarget();
        var rule = new Kosher.Log.Model.Rule.LogRule("unity logger", LogLevel.Debug, logger);
        logConfiguration.AddRule("unity console", rule);
        LogBuilder.Configuration(logConfiguration);
        LogBuilder.Build();

        if (Application.platform == RuntimePlatform.WindowsEditor)
        {
            TemplateLoader.Load(Path.Combine(Application.streamingAssetsPath, "Datas"));
        }
        else if(Application.platform == RuntimePlatform.Android)
        {
            TemplateLoader.Load(LoadAndroidAssetFile);
        }

        TemplateLoader.MakeRefTemplate();        
    }
    private string LoadAndroidAssetFile(string fileName)
    {
        var uri = new System.Uri(Path.Combine(Application.streamingAssetsPath, "Datas", fileName));
        var requester = UnityWebRequest.Get(uri);
        requester.SendWebRequest();
        while(requester.isDone == false)
        {
        }
        return requester.downloadHandler.text;
    }
    public override void BindScene(BaseScene baseScene)
    {
        scene = baseScene as TitleScene;
    }
    public void Init(ServerType serverType)
    {
        ApplicationManager.Instance.Init(serverType);
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
            Account = PlayerManager.Instance.GetUserData().Account,
            Token = ApplicationManager.Instance.GetUserToken()
        });

        if (response.Ok == false)
        {
            return false;
        }

        CharacterManager.Instance.Init(response.CharacterDatas);
        UserAssetManager.Instance.Init(response.AssetData);

        return true;
    }
}
