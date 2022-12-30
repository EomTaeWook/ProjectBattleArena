public interface IBattleEventHandler
{
    void Process(TickPassedEvent tickPassedEvent);

    void Process(StartSkillEvent startSkillEvent);

    void Process(EndSkillEvent endSkillEvent);
}
