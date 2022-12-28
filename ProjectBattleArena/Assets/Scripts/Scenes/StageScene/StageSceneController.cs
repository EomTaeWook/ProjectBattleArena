using Assets.Scripts.Scenes;

public class StageSceneController : SceneController<StageSceneController>
{
    StageScene scene;
    public override void BindScene(BaseScene baseScene)
    {
        scene = baseScene as StageScene;
    }

    public void Init()
    {

    }

    
}
