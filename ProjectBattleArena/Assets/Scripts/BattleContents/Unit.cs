using DataContainer.Generated;
using Protocol.GameWebServerAndClient.ShareModels;
using ShareLogic;
using System.Numerics;

public class Unit
{
    private int _skillAttacked = 0;

    private int _skillIndex = 0;

    private int _ticks = 0;

    private Vector2 _position;

    private CharacterTemplate _characterTemplate;

    public UnitStats _unitStats;
    public Unit(CharacterData characterData,
        CharacterTemplate characterTemplate)
    {
        _characterTemplate = characterTemplate;

        _unitStats = new UnitStats(characterTemplate, characterData);
    }

    public void OnTickPassed()
    {
        _ticks++;
    }
    public void DoAction()
    {
        
    }

    public void OnDamaged(Unit attacker, Damage damage)
    {
        
    }

}