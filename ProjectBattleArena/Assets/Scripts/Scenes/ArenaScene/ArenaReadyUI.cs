using Assets.Scripts.Internal;
using System.Threading.Tasks;
using UnityEngine;

public class ArenaReadyUI : UIItem
{
    public async void OnOpponentClickAsync(int index)
    {
       await ArenaBattleSceneController.Instance.RequestChallengeArena(index);
    }
    public override void DisposeUI()
    {
        this.gameObject.SetActive(false);
    }
}
