using Protocol.GameWebServerAndClient.ShareModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

public class Party
{
    public Battle Battle { get; private set; }
    private readonly List<Unit> _units = new List<Unit>();
    public Party(List<CharacterData> characterDatas)
    {
        foreach(var item in characterDatas)
        {
            _units.Add(new Unit(item));
        }
    }
    public ReadOnlyCollection<Unit> GetUnits()
    {
        return new ReadOnlyCollection<Unit>(_units);
    }
    public bool IsExist(Unit unit)
    {
        for(int i=0; i<_units.Count; ++i)
        {
            if (_units[i] == unit)
            {
                return true;
            }
        }
        return false;
    }
    public ICollection<Unit> GetAliveTargets()
    {
        var list = new List<Unit>();

        foreach(var unit in _units)
        {
            if(unit.IsDead() == true)
            {
                continue;
            }
            list.Add(unit);
        }

        return list;

    }
    public void SetBattle(Battle battle)
    {
        if(battle == null)
        {
            throw new ArgumentNullException(nameof(battle));
        }
        this.Battle = battle;
        foreach(var item in _units)
        {
            item.SetBattle(battle);
        }
    }
    public void DoAction(int currentTicks)
    {
        foreach(var unit in _units)
        {
            unit.OnTickPassed();
        }
        foreach (var unit in _units)
        {
            unit.DoAction();
        }
    }
}