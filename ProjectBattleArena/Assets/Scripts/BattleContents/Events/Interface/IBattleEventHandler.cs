namespace GameContents
{
    public interface IBattleEventHandler
    {
        void Process(TickPassedEvent tickPassedEvent);
        void Process(Unit unit, StartSkillEvent startSkillEvent);
        void Process(Unit unit, EndSkillEvent endSkillEvent);
        void Process(Unit unit, DamageEvent damageEvent);
        void Process(Unit unit, RemoveAbnormalStatusEvent removeAbnormalStatusEvent);
        void Process(Unit unit, AddAbnormalStatusEvent addAbnormalStatusEvent);
        void Process(Unit unit, DieEvent dieEvent);
        void Process(EndBattleEvent endBattleEvent);
    }
}
