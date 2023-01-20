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
        this.SceneModel.MainSceneUI = MainSceneUI.Instantiate();
    }

    public void CharacterUI(bool isShow)
    {
        if (this.SceneModel.Character != null)
        {
            this.SceneModel.Character.transform.SetParent(characterLayer.transform, false);
            this.SceneModel.Character.gameObject.SetActive(isShow);
        }
        characterLayer.gameObject.SetActive(isShow);
    }
    public void BattleUI(bool isShow)
    {
        gameManager.gameObject.SetActive(isShow);
    }
    public override void OnDestroyScene()
    {
        this.SceneModel.MainSceneUI.DisposeUI();
    }

}