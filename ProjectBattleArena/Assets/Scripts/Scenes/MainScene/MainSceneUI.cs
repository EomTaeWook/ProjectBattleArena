using Assets.Scripts.Internal;
using Kosher.Log;
using UnityEngine;

public class MainSceneUI : UIItem
{
    [SerializeField]
    MainHomeUI lobbyHomeUI;
    [SerializeField]
    MainCharacterUI characterUI;

    public static MainSceneUI Instantiate()
    {
        var prefab = ResourceManager.Instance.LoadAsset<MainSceneUI>($"Prefabs/Main/MainSceneUI");

        if (prefab == null)
        {
            LogHelper.Error($"no prefab");
            return null;
        }

        var item = UIManager.Instance.AddUI(prefab);
        return item.GetComponent<MainSceneUI>();
    }

    private void Awake()
    {
        characterUI.gameObject.SetActive(false);
        LoadHomeUI();
    }

    public void LoadCharacterUI()
    {
        lobbyHomeUI.gameObject.SetActive(false);
        characterUI.gameObject.SetActive(true);
        characterUI.Init();
        MainSceneController.Instance.ChangedUI(MainSceneController.UIType.Character);
    }
    public void LoadHomeUI()
    {
        lobbyHomeUI.gameObject.SetActive(true);
        characterUI.gameObject.SetActive(false);
        MainSceneController.Instance.ChangedUI(MainSceneController.UIType.Home);
    }
    public void LoadSkillSettingUI()
    {
        lobbyHomeUI.gameObject.SetActive(false);
        characterUI.gameObject.SetActive(false);
    }
    public void Refresh()
    {
        characterUI.Refresh();
    }
}