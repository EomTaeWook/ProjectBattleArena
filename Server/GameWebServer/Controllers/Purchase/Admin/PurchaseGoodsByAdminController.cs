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
        public override async Task<IGWCResponse> Process(TokenData tokenData, PurchaseGoodsByAdmin request)
        {
            var goodsTemplate = TemplateContainer<GoodsTemplate>.Find(request.TemplateId);

            if (goodsTemplate.Invalid())
            {
                return MakeErrorMessage(tokenData.Account, $"invalid request");
            }

            var loadCharacter = await _characterRepository.LoadCharacter(tokenData.CharacterName);

            if (loadCharacter == null)
            {
                return MakeErrorMessage(tokenData.Account, $"failed to load character");
            }

            var characterTemplate = TemplateContainer<CharacterTemplate>.Find(loadCharacter.TemplateId);

            var rewards = await GoodsManager.Instance.PurchaseGoodsAsync(tokenData.Account,
                characterTemplate,
                goodsTemplate,
                false);

            if(rewards == null)
            {
                return MakeErrorMessage(tokenData.Account, $"failed to purchase goods");
            }

            var gived = await RewardGiverManager.Instance.GiveRewradsAsync(tokenData.Account,
                tokenData.CharacterName,
                rewards,
                GiveRewardReason.PurchasedGoodsByAdmin);

            if (gived == false)
            {
                return MakeErrorMessage(tokenData.Account, $"failed to gived reward");
            }

            return new PurchaseGoodsByAdminResponse()
            {
                Ok = true,
                RewardDiff = rewards
            };
        }
    }
}
