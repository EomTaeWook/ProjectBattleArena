using Assets.Scripts.Internal;
using DataContainer.Generated;
using Kosher.Log;
using TemplateContainers;
using UnityEngine;

public class MainHomeUI : UIComponent
{
    public static MainHomeUI Instantiate(Transform parent)
    {
        var prefab = ResourceManager.Instance.LoadAsset<MainHomeUI>($"Prefabs/Lobby/LobbyHomeUI");

        if (prefab == null)
        {
            LogHelper.Error($"no prefab");
            return null;
        }

        var item = UIManager.Instance.AddUI(prefab, parent);
        return item.GetComponent<MainHomeUI>();
    }
    public void OnArenaButtonClick()
    {
        MainSceneController.Instance.Dispose();

        SceneManager.Instance.LoadScene(Assets.Scripts.Models.SceneType.ArenaBattleScene);
    }
    public async void OnGachaSkillButtonClickAsync(int count)
    {
        GoodsTemplate goodsTemplate;
        if (count == 1)
        {
            goodsTemplate = TemplateContainer<GoodsTemplate>.Find("GachaSkill");
        }
        else
        {
            goodsTemplate = TemplateContainer<GoodsTemplate>.Find("GachaSkillX10");
        }

        await MainSceneController.Instance.RequestPurchaseGoodsAsync(goodsTemplate);

    }
    public async void OnPurchaseCashButtonClickAsync()
    {
        var goodsTemplate = TemplateContainer<GoodsTemplate>.Find("CashX100000");

        await MainSceneController.Instance.RequestPurchaseGoodsAsync(goodsTemplate);
    }
}