namespace GameContents
{
    public interface IBattleEventHandler
    {
        void Process(TickPassedEvent tickPassedEvent);

        void Process(StartSkillEvent startSkillEvent);

        void Process(EndSkillEvent endSkillEvent);

        void Process(DamageEvent damageEvent);

        void Process(RemoveAbnormalStatusEvent removeAbnormalStatusEvent);

        void Process(AddAbnormalStatusEvent addAbnormalStatusEvent);
    }
}
