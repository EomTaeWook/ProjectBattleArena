using BA.GameServer.Game;
using BA.Repository;
using DataContainer.Generated;
using GameWebServer.Manager;
using Protocol.GameWebServerAndClient;
using Protocol.GameWebServerAndClient.ShareModels;
using TemplateContainers;

namespace GameWebServer.Controllers.Arena
{
    public class ChallengeArenaController : AuthAPIController<ChallengeArena>
    {
        private readonly UserAssetRepository _userAssetRepository;
        public ChallengeArenaController(UserAssetRepository userAssetRepository)
        {
            _userAssetRepository = userAssetRepository;
        }

        public override async Task<IGWCResponse> Process(TokenData tokenData, ChallengeArena request)
        {
            var loadAsset = await _userAssetRepository.LoadUserAssetAsync(tokenData.Account);

            if (loadAsset == null)
            {
                return MakeErrorMessage(tokenData.Account, $"failed to load user asset");
            }
            var arenaMaxCount = TemplateContainer<ConstantTemplate>.Find("ArenaTicketMaxCount");
            if (arenaMaxCount.Invalid())
            {
                return MakeErrorMessage(tokenData.Account, $"template is invalid");
            }
            var arenaTicketChargedCoolTime = TemplateContainer<ConstantTemplate>.Find("ArenaTicketChargedCoolTime");
            if (arenaTicketChargedCoolTime.Invalid())
            {
                return MakeErrorMessage(tokenData.Account, $"template is invalid");
            }

            var randomSeed = DateTime.Now.Ticks.GetHashCode();

            var needUpdate = AssetManager.Instance.ChargedResource(loadAsset.ArenaTicket,
                -1,
                arenaMaxCount.Value,
                arenaTicketChargedCoolTime.Value,
                loadAsset.ArenaTicketLatestTime,
                out int updateValue,
                out int remainChargedTime);

            if (updateValue < 0)
            {
                return MakeErrorMessage(tokenData.Account, $"not enough tickets");
            }

            var loadOpponentCharacter = await CharacterManager.Instance.LoadCharacterByUniqueIdAsync(request.OpponentId);

            if (loadOpponentCharacter == null)
            {
                return MakeErrorMessage(tokenData.Account, $"not found character id");
            }

            var loadMyCharacter = await CharacterManager.Instance.LoadCharacterAsync(tokenData.CharacterName);
            if (loadMyCharacter == null)
            {
                return MakeErrorMessage(tokenData.Account, $"not found character id");
            }
            
            if (needUpdate == true)
            {
                var updated = await AssetManager.Instance.ModifyArenaTicketAsync(tokenData.Account,
                    loadAsset.ArenaTicket,
                    updateValue,
                    DateTime.Now.Ticks,
                    ChangedAssetReason.ByUser);

                if (updated == false)
                {
                    return MakeErrorMessage(tokenData.Account, $"failed to update user asset");
                }
            }
            else
            {
                var updated = await AssetManager.Instance.ModifyArenaTicketAsync(tokenData.Account,
                    loadAsset.ArenaTicket,
                    updateValue,
                    loadAsset.ArenaTicketLatestTime,
                    ChangedAssetReason.ByUser);

                if (updated == false)
                {
                    return MakeErrorMessage(tokenData.Account, $"failed to update user asset");
                }
            }

            var battle = new BattleResource(-1,
                new List<CharacterData>()
                {
                    loadMyCharacter
                },
                new List<CharacterData>()
                {
                    loadOpponentCharacter,
                },
                () => { return new BattleEventHandler(); }
            );



            return new ChallengeArenaResponse()
            {
                Ok = true,
                OpponentCharacterData = loadOpponentCharacter,
                ArenaTicketRemainChargeTime = remainChargedTime,
                RandomSeed = randomSeed,
                AreanTicketDiff = -1,
                RewardData = new RewardData()
                {
                },
            };
        }
    }
}
