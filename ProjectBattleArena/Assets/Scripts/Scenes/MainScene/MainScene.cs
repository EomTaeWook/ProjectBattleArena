using UnityEngine;

public class MainScene : BaseScene<MainSceneModel>
{
    [SerializeField]
    private Transform characterLayer;
    [SerializeField]
    private GameManager gameManager;
    public override void OnAwakeScene()
    {
        MainSceneController.Instance.BindScene(this);
        this.SceneModel.LobbySceneUI = MainSceneUI.Instantiate();
    }

    public void CharacterUI(bool isShow)
    {
        if (this.SceneModel.Character != null)
        {
            this.SceneModel.Character.transform.SetParent(characterLayer.transform, false);
        }
        characterLayer.gameObject.SetActive(isShow);
    }
    public void BattleUI(bool isShow)
    {
        gameManager.gameObject.SetActive(isShow);
    }
    public override void OnDestroyScene()
    {
    }

}