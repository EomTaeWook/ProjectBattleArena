using Assets.Scripts.Internal;
using Kosher.Coroutine;
using Kosher.Unity;
using Protocol.GameWebServerAndClient.ShareModels;
using System.Collections;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private Battle _currentBattle;

    private CoroutineWorker _coroutineWorker = new CoroutineWorker();
    public void MakeBattle(
        int seed,
        CharacterData ally,
        CharacterData enemy)
    {
        _currentBattle = BattleManager.Instance.MakeBattle(seed, ally, enemy);
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

        }

        yield return null;
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
