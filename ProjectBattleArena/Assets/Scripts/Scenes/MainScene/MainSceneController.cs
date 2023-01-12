using Assets.Scripts.Internal;
using Assets.Scripts.Scenes;
using DataContainer.Generated;
using Protocol.GameWebServerAndClient;
using System.Threading.Tasks;
using TemplateContainers;

public class MainSceneController : SceneController<MainSceneController>
{
    MainScene scene;

    public enum UIType
    {
        Home,
        Character,
        Battle,

        Max,
    }

    public override void BindScene(BaseScene baseScene)
    {
        scene = baseScene as MainScene;
    }
    public void ChangedUI(UIType current)
    {
        if(scene.SceneModel.Character.IsNull() == false)
        {
            scene.SceneModel.Character.Recycle();
            scene.SceneModel.Character = null;
        }
        if(current == UIType.Character)
        {
            scene.SceneModel.Character.Recycle();
            var templateId = CharacterManager.Instance.SelectedCharacterData.TemplateId;
            scene.SceneModel.Character = ResourceManager.Instance.LoadCharcterAsset(templateId);

            scene.CharacterUI(true);
        }
        else if(current == UIType.Battle)
        {
            scene.CharacterUI(false);
            scene.BattleUI(true);
        }
    }
    public void RequestGachaSkill()
    {
        //var goodsTemplate = TemplateContainer<GoodsTemplate>.Find("GachaSkill");
        //var request = new PurchaseGoods
        //{
        //    TemplateId = goodsTemplate.Id,
        //    CharacterName = CharacterManager.Instance.SelectedCharacterData.CharacterName
        //};

        //var response = await HttpRequestHelper.Request<PurchaseGoods, PurchaseGoodsResponse>(request);

        //if (response.Ok == false)
        //{
        //    UIManager.Instance.ShowAlert("알림", "상품 구매에 실패하였습니다.");
        //    return;
        //}
    }
    
    public async Task RequestPurchaseGoodsAsync(GoodsTemplate goodsTemplate)
    {
        if (goodsTemplate.GoodsCategory == DataContainer.GoodsCategory.Cash)
        {
#if UNITY_EDITOR
            var purchaseGoodsByAdmin = new PurchaseGoodsByAdmin
            {
                TemplateId = goodsTemplate.Id,
                CharacterName = CharacterManager.Instance.SelectedCharacterData.CharacterName
            };
            var res = await HttpRequestHelper.AuthRequest<PurchaseGoodsByAdmin, PurchaseGoodsByAdminResponse>(purchaseGoodsByAdmin);

            if (res.Ok == false)
            {
                UIManager.Instance.ShowAlert("알림", "상품 구매에 실패하였습니다.");
                return;
            }

            UserAssetManager.Instance.Update(res.RewardDiff);
#else
        
#endif
        }
        else
        {
            var purchaseGoods = new PurchaseGoods
            {
                TemplateId = goodsTemplate.Id,
                CharacterName = CharacterManager.Instance.SelectedCharacterData.CharacterName,
            };
            var res = await HttpRequestHelper.AuthRequest<PurchaseGoods, PurchaseGoodsResponse>(purchaseGoods);

            if (res.Ok == false)
            {
                UIManager.Instance.ShowAlert("알림", "상품 구매에 실패하였습니다.");
                return;
            }

            UserAssetManager.Instance.Update(res.RewardDiff);
        }


    }
    public void Dispose()
    {
        if (scene.SceneModel.Character.IsNull() == false)
        {
            scene.SceneModel.Character.Recycle();
            scene.SceneModel.Character = null;
        }
    }
}