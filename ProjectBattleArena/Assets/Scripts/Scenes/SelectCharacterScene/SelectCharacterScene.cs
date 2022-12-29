using Assets.Scripts.Internal;
using TMPro;
using UnityEngine;

public class SelectCharacterScene : BaseScene<SelectCharacterSceneModel>
{
    [SerializeField]
    private TextMeshProUGUI selectCharacterName;
    [SerializeField]
    private GameObject characterSlotContent;
    [SerializeField]
    private Transform characterTransform;
    public override void OnAwakeScene()
    {
        SelectCharacterSceneController.Instance.BindScene(this);
        SelectCharacterSceneController.Instance.Init();
    }
    public void Refresh()
    {
        foreach(var item in this.SceneModel.Slots)
        {
            item.transform.SetParent(characterSlotContent.transform, false);
            item.gameObject.SetActive(true);
        }
        RefreshCharacter();
    }
    public void OnGameStartClick()
    {
        SelectCharacterSceneController.Instance.Dispose();
        SceneManager.Instance.LoadScene(Assets.Scripts.Models.SceneType.LobbyScene);
    }
    public void RefreshCharacter()
    {
        SceneModel.SelectCharacter.transform.SetParent(characterTransform.transform, false);
        SceneModel.SelectCharacter.SetActive(true);
    }
    public override void OnDestroyScene()
    {
        
    }
}
