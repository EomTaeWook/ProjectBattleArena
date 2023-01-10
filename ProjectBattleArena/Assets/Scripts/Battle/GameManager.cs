using Assets.Scripts.Internal;
using GameContents;
using Kosher.Coroutine;
using Protocol.GameWebServerAndClient.ShareModels;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private Battle _currentBattle;

    private CoroutineWorker _coroutineWorker = new CoroutineWorker();
    private BattleEventHandler _eventHandler;
    public void MakeBattle(
        int seed,
        CharacterData ally,
        CharacterData enemy)
    {
        _currentBattle = BattleManager.Instance.MakeBattle(seed, ally, enemy);
        _eventHandler = _currentBattle.GetBattleEventHandler() as BattleEventHandler;
    }

    private void MakeCharacter()
    {
        foreach(var item in _currentBattle.GetAllyParty().GetUnits())
        {
            var go = ResourceManager.Instance.LoadCharcterAsset(item.CharacterTemplate);


        }
        foreach(var item in _currentBattle.GetEnemyParty().GetUnits())
        {
            var go = ResourceManager.Instance.LoadCharcterAsset(item.CharacterTemplate);


        }
    }


    private IEnumerator ProcessBattle()
    {
        while(_currentBattle.IsBattleEnd () == false)
        {
            _currentBattle.ProcessTicks();

            var events = _eventHandler.GetInvokedEvents();
            ProcessEvents(events);
        }

        yield return null;
    }
    private void ProcessEvents(List<Tuple<Unit, BattleEvent>> events)
    {
        foreach (var item in events)
        {

        }
    }

    public void ReleaseBattle()
    {
        _coroutineWorker.StopAll();
        _currentBattle = null;
    }

    private void FixedUpdate()
    {
        _coroutineWorker.WorksUpdate(Time.deltaTime);
    }
}
