using Assets.Scripts.Internal;

public class TitleScene : BaseScene
{
    public override void OnAwakeScene()
    {        
        TitleSceneController.Instance.BindScene(this);
        TitleSceneController.Instance.Init();
    }

    public override void OnDestroyScene()
    {
    }

    public async void OnStartButtonClickAsync()
    {
        var init = await TitleSceneController.Instance.ReqeustSecurityKeyAsync();

        if(init == false)
        {
            UIManager.Instance.ShowAlert("알림", "초기화에 실패하였습니다.");
            return;
        }

        if (PlayerManager.Instance.IsExists() == false)
        {
            var maked = await PlayerManager.Instance.MakeAccountAsync();
            if (maked == false)
            {
                UIManager.Instance.ShowAlert("알림", "캐릭터 생성에 실패하였습니다.");
                return;
            }
        }

        var login = await TitleSceneController.Instance.RequestLoginAsync();

        if(login == false)
        {
            UIManager.Instance.ShowAlert("알림", "로그인에 실패하였습니다.");
            return;
        }

        if (CharacterManager.Instance.CharacterCount > 0)
        {
            SceneManager.Instance.LoadScene(Assets.Scripts.Models.SceneType.SelectCharacterScene);
        }
        else
        {
            SceneManager.Instance.LoadScene(Assets.Scripts.Models.SceneType.CreateCharacterScene);
        }
    }
}
