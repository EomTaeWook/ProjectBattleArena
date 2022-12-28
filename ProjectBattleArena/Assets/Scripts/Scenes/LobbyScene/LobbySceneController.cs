using Assets.Scripts.Internal;
using Assets.Scripts.Scenes;

public class LobbySceneController : SceneController<LobbySceneController>
{
    LobbyScene scene;

    public enum LobbyCurrentType
    {
        Home,
        Character,
        Battle,

        Max,
    }

    private LobbyCurrentType currentType;

    public override void BindScene(BaseScene baseScene)
    {
        scene = baseScene as LobbyScene;
    }
}