using Assets.Scripts.Scenes;
using Protocol.GameWebServerAndClient;
using Protocol.GameWebServerAndClient.ShareModels;
using System;
using System.Threading.Tasks;

public class ArenaBattleSceneController : SceneController<ArenaBattleSceneController>
{
    ArenaBattleScene scene;
    public override void BindScene(BaseScene baseScene)
    {
        scene = baseScene as ArenaBattleScene;
    }
    
    public async Task RequestChallengeArena(int index)
    {
        //var request = new ChallengeArena()
        //{
        //    OpponentId = "dummy"
        //};
        //var response = await HttpRequestHelper.AuthRequest<ChallengeArena, ChallengeArenaResponse>(request);

        //if (response.Ok == false)
        //{

        //}
        scene.HideReadyUI();

        scene.SceneModel.BattleInfoModel = new BattleInfoModel()
        {
            OpponentCharacterData = new CharacterData()
            {
                CharacterName = "Dummy",
                MountingSkillDatas = new System.Collections.Generic.List<long>(),
                SkillDatas = new System.Collections.Generic.List<SkillData>(),
                TemplateId = 1001,
                UniqueId = "test"
            },
            RandomSeed = DateTime.Now.GetHashCode()
        };

        scene.BattleStart();
    }


}
