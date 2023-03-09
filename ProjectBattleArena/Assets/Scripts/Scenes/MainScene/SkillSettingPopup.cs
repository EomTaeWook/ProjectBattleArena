using Assets.Scripts.Internal;
using DataContainer.Generated;
using Kosher.Log;
using Kosher.Unity;
using Protocol.GameWebServerAndClient.ShareModels;
using System.Collections.Generic;
using System.Threading.Tasks;
using TemplateContainers;
using UnityEngine;
using UnityEngine.UI;

public class SkillSettingPopup : UIItem
{
    [SerializeField]
    GridLayoutGroup gridLayout;
    [SerializeField]
    SkillUIItem slotSkillUiItem;

    private List<SkillUIItem> skillUIItems = new List<SkillUIItem>();
    private int slotIndex = 0;
    public static SkillSettingPopup Instantiate()
    {
        var prefab = ResourceManager.Instance.LoadAsset<SkillSettingPopup>($"Prefabs/Main/SkillSettingPopup");

        if (prefab == null)
        {
            LogHelper.Error($"no prefab");
            return null;
        }

        var item = UIManager.Instance.AddPopupUI(prefab);
        return item.GetComponent<SkillSettingPopup>();
    }
    public void Init(int slotIndex)
    {
        this.slotIndex = slotIndex;
        var mountingSkillDatas = CharacterManager.Instance.SelectedCharacterData.MountingSkillDatas;
        var containTemplate = new List<int>();
        for(int i=0; i< mountingSkillDatas.Count; ++i)
        {
            var mountingSkillData = CharacterManager.Instance.GetSkillData(mountingSkillDatas[i]);
            if(mountingSkillData == null)
            {
                continue;
            }
            containTemplate.Add(mountingSkillData.TemplateId);
            if(i == slotIndex)
            {
                slotSkillUiItem.Refresh(mountingSkillData);
            }
        }
        
        foreach (var item in CharacterManager.Instance.GetSkillDatas())
        {
            if(containTemplate.Contains(item.TemplateId) == true)
            {
                continue;
            }

            var uiItem = SkillUIItem.Instantiate();
            uiItem.Click += UiItem_SkillIconClick;
            uiItem.transform.SetParent(gridLayout.transform, false);
            uiItem.Refresh(item);
            skillUIItems.Add(uiItem);           
        }
    }

    private void UiItem_SkillIconClick(SkillUIItem sender)
    {
        var template = TemplateContainer<SkillsTemplate>.Find(sender.SkillData.TemplateId);
        UIManager.Instance.ShowConfirmAlert("스킬 선택",
            $"{template.Name} 교체하시겠습니까?",
            async () =>
            {
                await OnSelectedAsync(this.slotIndex, sender.SkillData);
            });
    }
    public async Task OnSelectedAsync(int slotIndex, SkillData skillData)
    {
        var result = await MainSceneController.Instance.RequestMountingSkill(slotIndex, skillData.Id);

        if (result == false)
        {
            UIManager.Instance.ShowAlert("알림", "스킬 교체에 실패하였습니다.");
            return;
        }
        this.CloseUI();
    }
    public void CloseUI()
    {
        foreach(var item in skillUIItems)
        {
            item.Click -= UiItem_SkillIconClick;
            item.Recycle<SkillUIItem>();
        }
        skillUIItems.Clear();
        this.DisposeUI();
    }
}