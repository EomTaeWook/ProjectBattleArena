using Assets.Scripts.Internal;
using Assets.Scripts.Scenes;
using DataContainer.Generated;
using Protocol.GameWebServerAndClient;
using System.Threading.Tasks;

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

        if (scene.SceneModel.Character.IsNull() == false)
        {
            scene.SceneModel.Character.Recycle();
            scene.SceneModel.Character = null;
        }
    }
    public void ChangeHomeUI()
    {

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

    public async Task<bool> RequestMountingSkill(int slotIndex,
        long skillId)
    {
        var request = new MountingSkill()
        {
            SlotIndex = slotIndex,
            SkillId  = skillId
        };

        var res = await HttpHelper.AuthRequest<MountingSkill, MountingSkillResponse>(request);

        if(res.Ok == false)
        {
            return false;
        }
        CharacterManager.Instance.SelectedCharacterData.MountingSkillDatas[res.SlotIndex] = res.ChangedSkillId;
        scene.SceneModel.MainSceneUI.Refresh();
        return true;
    }

    public async Task RequestPurchaseGoodsAsync(GoodsTemplate goodsTemplate)
    {
        if (goodsTemplate.GoodsCategory == DataContainer.GoodsCategory.Cash)
        {
            var purchaseGoodsByAdmin = new PurchaseGoodsByAdmin
            {
                TemplateId = goodsTemplate.Id,
            };
            var res = await HttpHelper.AuthRequest<PurchaseGoodsByAdmin, PurchaseGoodsByAdminResponse>(purchaseGoodsByAdmin);

            if (res.Ok == false)
            {
                UIManager.Instance.ShowAlert("알림", "상품 구매에 실패하였습니다.");
                return;
            }
            RewardManager.Instance.Update(res.RewardDiff);
        }
        else
        {
            var purchaseGoods = new PurchaseGoods
            {
                TemplateId = goodsTemplate.Id,
            };
            var res = await HttpHelper.AuthRequest<PurchaseGoods, PurchaseGoodsResponse>(purchaseGoods);

            if (res.Ok == false)
            {
                UIManager.Instance.ShowAlert("알림", "상품 구매에 실패하였습니다.");
                return;
            }

            RewardManager.Instance.Update(res.RewardDiff);
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