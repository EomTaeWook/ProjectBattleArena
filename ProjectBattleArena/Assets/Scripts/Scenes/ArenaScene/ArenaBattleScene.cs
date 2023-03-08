using Assets.Scripts.Internal;
using Kosher.Coroutine;
using Kosher.Unity;
using UnityEngine;

public class ArenaBattleScene : BaseScene<ArenaBattleSceneModel>
{
    [SerializeField]
    private GameManager gameManager;
    [SerializeField]
    private ArenaReadyUI readyUI;

    private CoroutineWorker _coroutineWorker = new CoroutineWorker();
    public override void OnAwakeScene()
    {
        ArenaBattleSceneController.Instance.BindScene(this);
        ShowReadyUI();
    }
    public void OnBtnBackClick()
    {
        SceneManager.Instance.LoadScene(Assets.Scripts.Models.SceneType.MainScene);
    }
    public override void OnDestroyScene()
    {
    }
    public void ShowReadyUI()
    {
        readyUI.gameObject.SetActive(true);
        gameManager.gameObject.SetActive(false);
    }
    public void HideReadyUI()
    {
        readyUI.gameObject.SetActive(false);
        gameManager.gameObject.SetActive(true);
    }

    public void BattleStart()
    {
        gameManager.MakeBattle(this.SceneModel.BattleInfoModel.RandomSeed,
            CharacterManager.Instance.SelectedCharacterData,
            this.SceneModel.BattleInfoModel.OpponentCharacterData);

        _coroutineWorker.Start(gameManager.BattleStart(), ReleaseBattle);
    }
    public void ReleaseBattle()
    {
        if(gameManager.IsWin() == true)
        {
            this.ShowAlert("알림", "승리했습니다.", () =>
            {
                ShowReadyUI();
            });
        }
        else 
        {
            this.ShowAlert("알림", "패배했습니다.", () => 
            {
                ShowReadyUI();
            });
        }
        gameManager.ReleaseBattle();
        
    }

    public void FixedUpdate()
    {
        _coroutineWorker.WorksUpdate(Time.deltaTime);
    }
}
