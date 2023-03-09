using UnityEngine;

public class MainCharacterUI : UIItem
{
    [SerializeField]
    SkillSettingUI skillSettingUI;
    public void Init()
    {
        skillSettingUI.gameObject.SetActive(false);
    }
    public void Refresh()
    {
        skillSettingUI.Refresh();
    }
    public void OnSkillButtonClick()
    {
        skillSettingUI.gameObject.SetActive(true);
        skillSettingUI.Init();
    }
}