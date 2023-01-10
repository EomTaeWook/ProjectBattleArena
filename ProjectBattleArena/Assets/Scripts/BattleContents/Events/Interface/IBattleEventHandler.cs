namespace GameContents
{
    public interface IBattleEventHandler
    {
        void Process(TickPassedEvent tickPassedEvent);
        void Process(Unit unit, StartSkillEvent startSkillEvent);

        void Process(Unit unit, StartCastingSkillEvent startCastingSkillEvent);
        void Process(Unit unit, EndSkillEvent endSkillEvent);
        void Process(Unit unit, EndCastingSkillEvent endCastingSkillEvent);
        void Process(Unit unit, StartSkillEffectEvent startSkillEffectEvent);
        void Process(Unit unit, EndSkillEffectEvent endSkillEffectEvent);
        void Process(Unit unit, DamageEvent damageEvent);
        void Process(Unit unit, RemoveAbnormalStatusEvent removeAbnormalStatusEvent);
        void Process(Unit unit, AddAbnormalStatusEvent addAbnormalStatusEvent);
        void Process(Unit unit, DieEvent dieEvent);
        void Process(EndBattleEvent endBattleEvent);
        void Process(Unit unit, MoveEvent moveEvent);
    }
}
