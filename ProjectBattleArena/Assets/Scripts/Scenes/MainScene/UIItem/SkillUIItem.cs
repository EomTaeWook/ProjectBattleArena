using Assets.Scripts.Internal;
using DataContainer.Generated;
using Kosher.Log;
using Kosher.Unity;
using Protocol.GameWebServerAndClient.ShareModels;
using TemplateContainers;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SkillUIItem : UIItem
{
    [SerializeField]
    TextMeshProUGUI txtSkillName;
    [SerializeField]
    private Image skillIcon;

    public delegate void EventHandler(SkillUIItem skillUIItem);
    public event EventHandler Click;
    public SkillData SkillData { get; private set; }
    public static SkillUIItem Instantiate()
    {
        var prefab = ResourceManager.Instance.LoadAsset<SkillUIItem>($"Prefabs/Main/SkillUIItem");

        if (prefab == null)
        {
            LogHelper.Error($"no prefab");
            return null;
        }
        var item = KosherUnityObjectPool.Instance.Pop<SkillUIItem>(prefab);
        item.gameObject.SetActive(true);
        return item;
    }
    public void Refresh(SkillData skill)
    {
        var template = TemplateContainer<SkillsTemplate>.Find(skill.TemplateId);
        if(template.Invalid())
        {
            LogHelper.Error($"template is validate");
            return;
        }
        SkillData = skill;
        txtSkillName.text = template.Name;
    }
    public void OnSkillButtonClick()
    {
        if (Click == null)
        {
            return;
        }
        this.Click.Invoke(this);
    }
}