using Assets.Scripts.Scenes;

public class ArenaSceneController : SceneController<TitleSceneController>
{
    ArenaScene scene;
    public override void BindScene(BaseScene baseScene)
    {
        scene = baseScene as ArenaScene;
    }

    public void Init()
    {

    }

    
}
