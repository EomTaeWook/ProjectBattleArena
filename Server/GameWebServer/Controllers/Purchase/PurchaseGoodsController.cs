using BA.Repository;
using DataContainer.Generated;
using GameWebServer.Manager;
using Protocol.GameWebServerAndClient;
using Protocol.GameWebServerAndClient.ShareModels;
using TemplateContainers;

namespace GameWebServer.Controllers.Purchase
{
    public class PurchaseGoodsController : AuthAPIController<PurchaseGoods>
    {
        private SkillRepository _skillRepository;
        private UserAssetRepository _assetRepository;
        private CharacterRepository _characterRepository;
        public PurchaseGoodsController(SkillRepository skillRepository,
            CharacterRepository characterRepository,
            UserAssetRepository userAssetRepository)
        {
            _skillRepository = skillRepository;
            _characterRepository = characterRepository;
            _assetRepository = userAssetRepository;
        }

        public override async Task<IGWCResponse> Process(string account, PurchaseGoods request)
        {
            var goodsTemplate = TemplateContainer<GoodsTemplate>.Find(request.TemplateId);

            if(goodsTemplate.Invalid())
            {
                return MakeErrorMessage(account, $"invalid request");
            }

            var loadCharacter = await _characterRepository.LoadCharacter(request.CharacterName);

            if(loadCharacter == null)
            {
                return MakeErrorMessage(account, $"failed to load character");
            }

            var characterTemplate = TemplateContainer<CharacterTemplate>.Find(loadCharacter.TemplateId);
            var rewards = await GoodsManager.Instance.PurchaseGoodsAsync(account,
                characterTemplate,
                goodsTemplate);

            if(rewards == null)
            {
                return MakeErrorMessage(account, $"failed to purchase goods");
            }

            var gived = await RewardGiverManager.Instance.GiveRewradsAsync(account,
                request.CharacterName,
                rewards,
                GiveRewardReason.PurchasedGoods);

            if(gived == false)
            {
                return MakeErrorMessage(account, $"failed to gived reward");
            }

            return new PurchaseGoodsResponse()
            {
                Ok = true,
                RewardDiff = rewards
            };
        }
    }
}
