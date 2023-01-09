using GameContents;
using System;
using System.Collections.Generic;

namespace BA.GameServer.Game
{
    public class BattleEventHandler : IBattleEventHandler
    {
        private List<Tuple<Unit, BattleEvent>> _invokedEvents = new List<Tuple<Unit, BattleEvent>>();
        public void Process(TickPassedEvent tickPassedEvent)
        {
            _invokedEvents.Add(Tuple.Create<Unit, BattleEvent>(null, tickPassedEvent));
        }

        public void Process(Unit unit, StartSkillEvent startSkillEvent)
        {
            _invokedEvents.Add(Tuple.Create<Unit, BattleEvent>(unit, startSkillEvent));
        }

        public void Process(Unit unit, EndSkillEvent endSkillEvent)
        {
            _invokedEvents.Add(Tuple.Create<Unit, BattleEvent>(unit, endSkillEvent));
        }

        public void Process(Unit unit, DamageEvent damageEvent)
        {
            _invokedEvents.Add(Tuple.Create<Unit, BattleEvent>(unit, damageEvent));
        }

        public void Process(Unit unit, RemoveAbnormalStatusEvent removeAbnormalStatusEvent)
        {
            _invokedEvents.Add(Tuple.Create<Unit, BattleEvent>(unit, removeAbnormalStatusEvent));
        }

        public void Process(Unit unit, AddAbnormalStatusEvent addAbnormalStatusEvent)
        {
            _invokedEvents.Add(Tuple.Create<Unit, BattleEvent>(unit, addAbnormalStatusEvent));
        }
        public List<Tuple<Unit, BattleEvent>> InvokedEvents()
        {
            return _invokedEvents;
        }

        public void Process(Unit unit, DieEvent dieEvent)
        {
            _invokedEvents.Add(Tuple.Create<Unit, BattleEvent>(unit, dieEvent));
        }

        public void Process(EndBattleEvent endBattleEvent)
        {
            _invokedEvents.Add(Tuple.Create<Unit, BattleEvent>(null, endBattleEvent));
        }

        public void Process(Unit unit, MoveEvent moveEvent)
        {
            _invokedEvents.Add(Tuple.Create<Unit, BattleEvent>(unit, moveEvent));
        }
    }
}
