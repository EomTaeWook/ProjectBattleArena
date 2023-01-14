using UnityEngine;

public class MainCharacterUI : UIComponent
{
    [SerializeField]
    SkillSettingUI skillSettingUI;
    public void Init()
    {
        skillSettingUI.gameObject.SetActive(false);
    }
    public void OnSkillButtonClick()
    {
        skillSettingUI.gameObject.SetActive(true);
        skillSettingUI.Init();
    }
}