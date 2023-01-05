using Kosher.Framework;
using Protocol.GameWebServerAndClient.ShareModels;
using ShareLogic;

namespace BA.GameServer.Game
{
    public class BattleResourceHolder : Singleton<BattleResourceHolder>
    {
        private readonly Incremental _incremental = new Incremental();
        private readonly Dictionary<long, BattleResource> _resource = new Dictionary<long, BattleResource>();
        private readonly object _syncObj = new object();
        public BattleResource CreateBattleResource(List<CharacterData> allies,
                                                List<CharacterData> enemies)
        {
            var id = _incremental.Increment();
            var battle = new BattleResource(id, allies, enemies);

            lock (_syncObj)
            {
                _resource.Add(id, battle);
            }
            return battle;
        }
        public void ReleaseBattle(long id)
        {
            lock (_syncObj)
            {
                _resource.Remove(id);
            }
        }
    }
}
