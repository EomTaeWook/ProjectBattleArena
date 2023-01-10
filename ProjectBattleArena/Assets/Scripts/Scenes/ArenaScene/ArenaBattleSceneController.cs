using Assets.Scripts.Scenes;

public class ArenaBattleSceneController : SceneController<ArenaBattleSceneController>
{
    ArenaBattleScene scene;
    public override void BindScene(BaseScene baseScene)
    {
        scene = baseScene as ArenaBattleScene;
    }

    public void Init()
    {

    }

    
}
