using DataContainer.Generated;

public abstract class BattleEvent
{
    public int Ticks { get; private set; }
    public int Id { get; private set; }
    public BattleEvent(int id, int ticks)
    {
        Ticks = ticks;
        Id = id;
    }
}
public class TickPassedEvent : BattleEvent
{
    public TickPassedEvent(int id, int ticks) : base(id, ticks)
    {
    }
}
public class StartSkillEvent : BattleEvent
{
    public SkillsTemplate SkillsTemplate { get; private set; }
    public StartSkillEvent(SkillsTemplate template, int id, int ticks) : base(id, ticks)
    {
        SkillsTemplate = template;
    }
}

public class EndSkillEvent : BattleEvent
{
    public SkillsTemplate SkillsTemplate { get; private set; }
    public EndSkillEvent(SkillsTemplate template, int id, int ticks) : base(id, ticks)
    {
        SkillsTemplate = template;
    }
}
