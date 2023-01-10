using BA.GameServer.Game;
using BA.GameServer.Modules.Game.Models;
using BA.Repository;
using BA.Repository.Helper;
using Kosher.Sockets;
using Kosher.Sockets.Interface;
using Protocol.GameWebServerAndClient.ShareModels;
using Protocol.GSC;
using Protocol.GSC.ShareModels;
using ShareLogic.Network;
using System.Collections.Generic;
using System.Text.Json;
using System.Threading.Tasks;

namespace BA.GameServer.Modules.Game.Handler
{
    public class GSCFuncHandler : EnumCallbackBinder<GSCFuncHandler, CGSProtocol, string>, ISessionComponent, IStringFuncHandler
    {
        public Session Session { get; private set; }
        private Player _player;
        public void SetSession(Session session)
        {
            Session = session;
        }
        public bool CheckProtocol(short protocol)
        {
            return base.CheckProtocol((CGSProtocol)protocol);
        }
        public void Execute(short protocol, string body)
        {
            base.Execute((CGSProtocol)protocol, body);
        }
        public async Task PvPBattleStartAsync(string body)
        {
            var packetData = JsonSerializer.Deserialize<PvPBattleStart>(body);

            var characterRepository = DBServiceHelper.Instance.GetService<CharacterRepository>();

            var response = new BattleStartResponse()
            {
                Ok = false,
            };

            var loadCharater = await characterRepository.LoadCharacter(_player.Nickname);
            if(loadCharater == null)
            {
                response.Reason = "failed to load character";
                _player.Send(GSCProtocol.BattleStartResponse,
                    response);
                return;
            }

            var loadOpponentCharater = await characterRepository.LoadCharacter(packetData.OpponentNickname);
            if(loadOpponentCharater == null)
            {
                response.Reason = "failed to load opponent character";
                _player.Send(GSCProtocol.BattleStartResponse,
                    response);
                return;
            }
            var allies = new List<CharacterData>() { loadCharater };
            var enemies = new List<CharacterData>() { loadOpponentCharater };

            var battleResource = BattleResourceHolder.Instance.CreateBattleResource(allies,
                enemies);

            response.Ok = true;
            response.Allies = allies;
            response.Enemies = enemies;
            response.RandomSeed = battleResource.RandomSeed;

            _player.Send(GSCProtocol.BattleStartResponse,
                    response);
        }
        public void JoinGameServer(string body)
        {
            var packetData = JsonSerializer.Deserialize<JoinGameServer>(body);

            _player = new Player(Session, packetData.Nickname);

            var added = PlayerHolder.Instance.Add(_player);
            //중복
            if(added == false)
            {
                var response = new JoinGameServerResponse()
                {
                    Reason = "the same nickname is online"
                };
                _player.Send(GSCProtocol.JoinGameServerResponse, response);
                Dispose();
            }
        }
        public override void Dispose()
        {
            base.Dispose();
            PlayerHolder.Instance.Remove(_player.Nickname);
            _player.Session.Shutdown();
            _player = null;
        }
    }
}

