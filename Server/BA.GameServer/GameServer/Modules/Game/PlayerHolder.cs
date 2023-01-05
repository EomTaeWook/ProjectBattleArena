using BA.GameServer.Modules.Game.Models;
using Kosher.Framework;

namespace BA.GameServer.Modules.Game
{
    public class PlayerHolder : Singleton<PlayerHolder>
    {
        public Dictionary<string, Player> _playerDatas = new Dictionary<string, Player>();
        private SpinLock _lock;
        public bool Add(Player player)
        {
            bool locked = false;
            _lock.Enter(ref locked);
            var added = _playerDatas.TryAdd(player.Nickname, player);
            _lock.Exit();

            return added;
        }
        public void Remove(string nickname)
        {
            bool locked = false;
            _lock.Enter(ref locked);
            _playerDatas.Remove(nickname);
            _lock.Exit();
        }
    }
}
