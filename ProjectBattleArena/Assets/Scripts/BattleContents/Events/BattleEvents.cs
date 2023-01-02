using DataContainer;
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
public class DamageEvent : BattleEvent
{
    public Damage Damage { get; private set; }

    public int ShildDamaged { get; private set; }
    public DamageEvent(
        Damage damage,
        int shildDamaged,
        int id,
        int ticks) : base(id, ticks)
    {
        Damage = damage;
        ShildDamaged = shildDamaged;
    }
}

public class AddAbnormalStatusEvent : BattleEvent
{
    public AbnormalStatusType AbnormalStatusType { get; private set; }

    public int DurationTicks { get; private set; }
    public AddAbnormalStatusEvent(AbnormalStatusType abnormalStatusType,
        int durationTicks,
        int id,
        int ticks) : base(id, ticks)
    {
        DurationTicks = durationTicks;
        AbnormalStatusType = abnormalStatusType;
    }
}

public class RemoveAbnormalStatusEvent : BattleEvent
{
    public AbnormalStatusType AbnormalStatusType { get; private set; }
    public RemoveAbnormalStatusEvent(AbnormalStatusType abnormalStatusType, int id, int ticks) : base(id, ticks)
    {
        AbnormalStatusType = abnormalStatusType;
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
