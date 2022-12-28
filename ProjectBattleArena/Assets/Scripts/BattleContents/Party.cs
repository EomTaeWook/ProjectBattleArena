using Protocol.GameWebServerAndClient.ShareModels;
using System.Collections.Generic;
using System;
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
    public void SetBattle(Battle battle)
    {
        if(battle == null)
        {
            throw new ArgumentNullException(nameof(battle));
        }
        this.Battle = battle;
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