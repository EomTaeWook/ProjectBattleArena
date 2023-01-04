using GameContents;

namespace BA.GameServer.Battle
{
    public class BattleEventHandler : IBattleEventHandler
    {
        private List<BattleEvent> _invokedEvents = new List<BattleEvent>();
        public void Process(TickPassedEvent tickPassedEvent)
        {
            _invokedEvents.Add(tickPassedEvent);
        }

        public void Process(StartSkillEvent startSkillEvent)
        {
            _invokedEvents.Add(startSkillEvent);
        }

        public void Process(EndSkillEvent endSkillEvent)
        {
            _invokedEvents.Add(endSkillEvent);
        }

        public void Process(DamageEvent damageEvent)
        {
            _invokedEvents.Add(damageEvent);
        }

        public void Process(RemoveAbnormalStatusEvent removeAbnormalStatusEvent)
        {
            _invokedEvents.Add(removeAbnormalStatusEvent);
        }

        public void Process(AddAbnormalStatusEvent addAbnormalStatusEvent)
        {
            _invokedEvents.Add(addAbnormalStatusEvent);
        }
    }
}
