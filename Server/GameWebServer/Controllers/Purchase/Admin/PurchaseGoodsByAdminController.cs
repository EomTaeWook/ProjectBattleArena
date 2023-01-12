using BA.Repository;
using DataContainer.Generated;
using GameWebServer.Manager;
using Protocol.GameWebServerAndClient;
using Protocol.GameWebServerAndClient.ShareModels;
using TemplateContainers;

namespace GameWebServer.Controllers.Purchase.Admin
{
    public class PurchaseGoodsByAdminController : AuthAPIController<PurchaseGoodsByAdmin>
    {
        private CharacterRepository _characterRepository;
        public PurchaseGoodsByAdminController(CharacterRepository characterRepository)
        {
            _characterRepository = characterRepository;
        }
        public override async Task<IGWCResponse> Process(string account, PurchaseGoodsByAdmin request)
        {
            var goodsTemplate = TemplateContainer<GoodsTemplate>.Find(request.TemplateId);

            if (goodsTemplate.Invalid())
            {
                return MakeErrorMessage(account, $"invalid request");
            }

            var loadCharacter = await _characterRepository.LoadCharacter(request.CharacterName);

            if (loadCharacter == null)
            {
                return MakeErrorMessage(account, $"failed to load character");
            }

            var characterTemplate = TemplateContainer<CharacterTemplate>.Find(loadCharacter.TemplateId);

            var rewards = await GoodsManager.Instance.PurchaseGoodsAsync(account,
                characterTemplate,
                goodsTemplate,
                false);

            if(rewards == null)
            {
                return MakeErrorMessage(account, $"failed to purchase goods");
            }

            var gived = await RewardGiverManager.Instance.GiveRewradsAsync(account,
                request.CharacterName,
                rewards,
                GiveRewardReason.PurchasedGoodsByAdmin);

            if (gived == false)
            {
                return MakeErrorMessage(account, $"failed to gived reward");
            }

            return new PurchaseGoodsByAdminResponse()
            {
                Ok = true,
                RewardDiff = rewards
            };
        }
    }
}
