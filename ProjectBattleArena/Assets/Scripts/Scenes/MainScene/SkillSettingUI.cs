using Assets.Scripts.Internal;
using DataContainer.Generated;
using Kosher.Log;
using TemplateContainers;
using UnityEngine;

public class SkillSettingUI : UIComponent
{
    [SerializeField]
    SkillUIItem[] skills;
    public void Init()
    {
        var mountingSkill = CharacterManager.Instance.SelectedCharacterData.MountingSkillDatas;

        for(int i=0; i< mountingSkill.Count; ++i)
        {
            var skillData = CharacterManager.Instance.GetSkillData(mountingSkill[i]);
            if(skillData != null)
            {
                skills[i].Refresh(skillData);
            }
        }
        foreach(var item in skills)
        {
            item.Click += Item_Click;
        }
    }

    private void Item_Click(SkillUIItem skillUIItem)
    {
        for(int i=0; i< skills.Length; ++i)
        {
            if (skills[i] == skillUIItem)
            {
                OnSkillSlotButtonClick(i);
                return;
            }
        }
    }
    
    public void OnSkillSlotButtonClick(int index)
    {
        var template = TemplateContainer<ConstantTemplate>.Find($"OpenSlot{index}");
        if(template.Invalid())
        {
            LogHelper.Error($"template is invalid");
            return;
        }

        if(template.Value > CharacterManager.Instance.Level)
        {
            UIManager.Instance.ShowAlert($"알림", $"{index + 1} 슬롯은 캐릭터 레벨 {template.Value}에 열립니다.");
            return;
        }

        var popup = SkillSettingPopup.Instantiate();
        popup.Init(index);
    }
    public void OnCloseButtonClick()
    {
        foreach (var item in skills)
        {
            item.Click -= Item_Click;
        }
        this.gameObject.SetActive(false);
    }
}