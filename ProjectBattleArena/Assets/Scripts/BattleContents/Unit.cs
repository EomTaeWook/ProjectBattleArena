using Protocol.GameWebServerAndClient.ShareModels;
using System.Numerics;

public class Unit
{
    private int _skillAttackPoint = 0;

    private int _skillIndex = 0;

    private int _ticks = 0;

    private Vector2 _position;

    private CharacterData _characterData;

    private int _range = 0;

    public UnitHpStats UnitStats { get; private set; }

    public Unit(CharacterData characterData)
    {
        _characterData = characterData;

        if(_characterData.Job == JobType.Wizard)
        {
            _range =1;
        }

        UnitStats = new UnitHpStats(100);
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