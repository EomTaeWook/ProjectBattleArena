using UnityEngine;

public class LobbyScene : BaseScene<LobbySceneModel>
{
    LobbyHomeUI lobbyHomeUI;
    LobbyCharacterUI characterUI;
    [SerializeField]
    Transform uiContentsTransform;

    public override void OnAwakeScene()
    {
        lobbyHomeUI = LobbyHomeUI.Instantiate(uiContentsTransform);
        characterUI = LobbyCharacterUI.Instantiate(uiContentsTransform);

        characterUI.gameObject.SetActive(false);
    }

    public override void OnDestroyScene()
    {

    }

    public void LoadCharacterUI()
    {
        lobbyHomeUI.gameObject.SetActive(false);
        characterUI.gameObject.SetActive(true);
    }
    public void LoadHomeUI()
    {
        lobbyHomeUI.gameObject.SetActive(true);
        characterUI.gameObject.SetActive(false);
    }
}