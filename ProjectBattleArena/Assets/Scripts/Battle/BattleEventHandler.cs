using System.Collections.Generic;

public class BattleEventHandler : IBattleEventHandler
{
    private List<BattleEvent> _battleEvents = new List<BattleEvent>();
    public void Process(TickPassedEvent tickPassedEvent)
    {
        _battleEvents.Add(tickPassedEvent);
    }

    public void Process(StartSkillEvent startSkillEvent)
    {
        _battleEvents.Add(startSkillEvent);
    }

    public void Process(EndSkillEvent endSkillEvent)
    {
        _battleEvents.Add(endSkillEvent);
    }

    public void Process(DamageEvent damageEvent)
    {
        _battleEvents.Add(damageEvent);
    }

    public void Process(RemoveAbnormalStatusEvent removeAbnormalStatusEvent)
    {
        _battleEvents.Add(removeAbnormalStatusEvent);
    }
    public void Process(AddAbnormalStatusEvent addAbnormalStatusEvent)
    {
        _battleEvents.Add(addAbnormalStatusEvent);
    }

    public List<BattleEvent> GetInvokedEvents()
    {
        return _battleEvents;
    }

}