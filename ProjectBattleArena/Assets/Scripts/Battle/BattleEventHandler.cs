using GameContents;
using System;
using System.Collections.Generic;

public class BattleEventHandler : IBattleEventHandler
{
    private readonly List<Tuple<Unit, BattleEvent>> _battleEvents = new List<Tuple<Unit, BattleEvent>>();
    
    public List<Tuple<Unit, BattleEvent>> GetInvokedEvents()
    {
        return _battleEvents;
    }

    public void Process(Unit unit, StartSkillEvent startSkillEvent)
    {
        _battleEvents.Add(Tuple.Create<Unit, BattleEvent>(unit, startSkillEvent));
    }

    public void Process(Unit unit, EndSkillEvent endSkillEvent)
    {
        _battleEvents.Add(Tuple.Create<Unit, BattleEvent>(unit, endSkillEvent));
    }

    public void Process(Unit unit, DamageEvent damageEvent)
    {
        _battleEvents.Add(Tuple.Create<Unit, BattleEvent>(unit, damageEvent));
    }

    public void Process(Unit unit, RemoveAbnormalStatusEvent removeAbnormalStatusEvent)
    {
        _battleEvents.Add(Tuple.Create<Unit, BattleEvent>(unit, removeAbnormalStatusEvent));
    }

    public void Process(Unit unit, AddAbnormalStatusEvent addAbnormalStatusEvent)
    {
        _battleEvents.Add(Tuple.Create<Unit, BattleEvent>(unit, addAbnormalStatusEvent));
    }

    public void Process(Unit unit, DieEvent dieEvent)
    {
        _battleEvents.Add(Tuple.Create<Unit, BattleEvent>(unit, dieEvent));
    }

    public void Process(EndBattleEvent endBattleEvent)
    {
        _battleEvents.Add(Tuple.Create<Unit, BattleEvent>(null, endBattleEvent));
    }

    public void Process(Unit unit, MoveEvent moveEvent)
    {
        _battleEvents.Add(Tuple.Create<Unit, BattleEvent>(null, moveEvent));
    }

    public void Process(TickPassedEvent tickPassedEvent)
    {
        _battleEvents.Add(Tuple.Create<Unit, BattleEvent>(null, tickPassedEvent));
    }

    public void Process(Unit unit, StartSkillEffectEvent startSkillEffectEvent)
    {
        _battleEvents.Add(Tuple.Create<Unit, BattleEvent>(null, startSkillEffectEvent));
    }

    public void Process(Unit unit, EndSkillEffectEvent endSkillEffectEvent)
    {
        _battleEvents.Add(Tuple.Create<Unit, BattleEvent>(null, endSkillEffectEvent));
    }

    public void Process(Unit unit, StartCastingSkillEvent startCastingSkillEvent)
    {
        _battleEvents.Add(Tuple.Create<Unit, BattleEvent>(unit, startCastingSkillEvent));
    }

    public void Process(Unit unit, EndCastingSkillEvent endCastingSkillEvent)
    {
        _battleEvents.Add(Tuple.Create<Unit, BattleEvent>(unit, endCastingSkillEvent));
    }
}