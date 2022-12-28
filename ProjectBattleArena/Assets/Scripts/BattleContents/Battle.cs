using Protocol.GameWebServerAndClient.ShareModels;
using ShareLogic.Random;
using System;
using System.Collections.Generic;

public class Battle
{
    private const int DefaultPerTIcks = 33; //33ms;
    private RandomGenerator _randomGenerator;
    private long _startedTime;
    private int _currentTicks;
    private Party allyParty;
    private Party enemyParty;
    public Battle(RandomGenerator randomGenerator,
        List<CharacterData> ally,
        List<CharacterData> enemy
        )
    {
        _randomGenerator = randomGenerator;
        allyParty = new Party(ally);
        enemyParty = new Party(enemy);
    }
    public void Init()
    {
        allyParty.SetBattle(this);
        enemyParty.SetBattle(this);
        _startedTime = DateTime.Now.Ticks;
    }
    public void ProcessTicks()
    {
        var elapsedTime = TimeSpan.FromTicks(DateTime.Now.Ticks - this._startedTime).TotalMilliseconds;

        int elapsedTickCount = (int)(elapsedTime / DefaultPerTIcks);
        
        for(int i=0; i< elapsedTickCount; ++i )
        {
            _currentTicks++;
            DoAction();
        }
    }

    public void DoAction()
    {
        allyParty.DoAction(_currentTicks);
        enemyParty.DoAction(_currentTicks);
    }

}