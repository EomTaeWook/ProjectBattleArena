using Protocol.GameWebServerAndClient.ShareModels;
using ShareLogic.Random;
using System;
using System.Collections.Generic;

public class Battle
{
    private const int DefaultPerTicks = 33; //33ms;
    private RandomGenerator _randomGenerator;
    private long _startedTime;
    private int _currentTicks;
    private Party allyParty;
    private Party enemyParty;
    private IBattleEventHandler _battleEventHandler;
    private int _battleEventIndex = 0;
    public Battle(IBattleEventHandler battleEventHandler,
        int randomSeed,
        List<CharacterData> ally,
        List<CharacterData> enemy
        )
    {
        _battleEventHandler = battleEventHandler;
        _randomGenerator = new RandomGenerator(randomSeed);
        allyParty = new Party(ally);
        enemyParty = new Party(enemy);
    }
    public void Init()
    {
        allyParty.SetBattle(this);
        enemyParty.SetBattle(this);
        _startedTime = DateTime.Now.Ticks;
    }
    public int GetBattleIndex()
    {
        return _battleEventIndex++;
    }
    public int GetCurrentTicks()
    {
        return _currentTicks;
    }

    
    public void ProcessTicks()
    {
        var elapsedTime = TimeSpan.FromTicks(DateTime.Now.Ticks - this._startedTime).TotalMilliseconds;

        int elapsedTickCount = (int)(elapsedTime / DefaultPerTicks);
        
        for(int i=0; i< elapsedTickCount; ++i )
        {
            TickPassedEvent tickPassedEvent = new TickPassedEvent(GetBattleIndex(), _currentTicks);
            _battleEventHandler.Process(tickPassedEvent);
            _currentTicks++;
            DoAction();
        }
    }
    public IBattleEventHandler GetBattleEventHandler()
    {
        return _battleEventHandler;
    }

    private void DoAction()
    {
        allyParty.DoAction(_currentTicks);
        enemyParty.DoAction(_currentTicks);
    }
}