using Assets.Scripts.Internal;
using Kosher.Unity;
using UnityEngine;

public class MainScene : BaseScene<MainSceneModel>
{
    [SerializeField]
    private Transform characterLayer;
    public override void OnAwakeScene()
    {
        MainSceneController.Instance.BindScene(this);
        this.SceneModel.LobbySceneUI = MainSceneUI.Instantiate();
    }

    public void Refresh()
    {
        if(this.SceneModel.Character != null)
        {
            this.SceneModel.Character.transform.SetParent(characterLayer.transform, false);
            this.SceneModel.Character.gameObject.SetActive(true);
        }
    }

    public override void OnDestroyScene()
    {
    }

}