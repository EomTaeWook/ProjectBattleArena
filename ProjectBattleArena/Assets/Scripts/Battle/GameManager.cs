using Assets.Scripts.Internal;
using GameContents;
using Protocol.GameWebServerAndClient.ShareModels;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private Battle _currentBattle;
    private BattleEventHandler _eventHandler;

    private List<GameObject> _allyUnits = new List<GameObject>();
    private List<GameObject> _enemyUnits = new List<GameObject>();

    public void MakeBattle(
        int seed,
        CharacterData ally,
        CharacterData enemy)
    {
        _currentBattle = BattleManager.Instance.MakeBattle(seed, ally, enemy);
        _eventHandler = _currentBattle.GetBattleEventHandler() as BattleEventHandler;
        MakeCharacter();
    }

    private void MakeCharacter()
    {
        var maxY = _currentBattle.GetMapMaxY() / 2;
        foreach (var item in _currentBattle.GetAllyParty().GetUnits())
        {
            var go = ResourceManager.Instance.LoadCharcterAsset(item.CharacterTemplate);
            go.transform.SetParent(this.transform, false);
            go.transform.position = new Vector3(item.Position.X, maxY - (item.Position.Y / 2.0F), 0);
            go.gameObject.SetActive(true);
            _allyUnits.Add(go);
        }
        foreach(var item in _currentBattle.GetEnemyParty().GetUnits())
        {
            var go = ResourceManager.Instance.LoadCharcterAsset(item.CharacterTemplate);
            go.transform.SetParent(this.transform, false);
            go.transform.SetPositionAndRotation(new Vector3(item.Position.X, maxY - (item.Position.Y / 2.0F), 1),
                new Quaternion(0, 180, 0, 0));
            go.gameObject.SetActive(true);
            _enemyUnits.Add(go);
        }
    }
    public bool IsWin()
    {
        return _currentBattle.IsWin();
    }
    public IEnumerator BattleStart()
    {
        return ProcessBattle();
    }
    private IEnumerator ProcessBattle()
    {
        while(_currentBattle.IsBattleEnd () == false)
        {
            _currentBattle.ProcessTicks();

            var events = _eventHandler.GetInvokedEvents();

            ProcessEvents(events);

            events.Clear();

            yield return null;
        }

        yield return null;
    }
    private void ProcessEvents(List<Tuple<Unit, BattleEvent>> events)
    {
        foreach (var item in events)
        {

        }
    }
    public bool IsEnd()
    {
        if(_currentBattle == null)
        {
            return true;
        }
        return _currentBattle.IsBattleEnd();
    }
    public void ReleaseBattle()
    {
        foreach(var item in _allyUnits)
        {
            item.Recycle();
        }
        _allyUnits.Clear();

        foreach (var item in _enemyUnits)
        {
            item.Recycle();
        }
        _enemyUnits.Clear();
        _currentBattle = null;
    }
}
