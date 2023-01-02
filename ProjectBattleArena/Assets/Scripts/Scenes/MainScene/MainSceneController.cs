using Assets.Scripts.Internal;
using Assets.Scripts.Scenes;

public class MainSceneController : SceneController<MainSceneController>
{
    MainScene scene;

    public enum UIType
    {
        Home,
        Character,
        Battle,

        Max,
    }

    public override void BindScene(BaseScene baseScene)
    {
        scene = baseScene as MainScene;
    }
    public void ChangedUI(UIType current)
    {
        
        if(scene.SceneModel.Character.IsNull() == false)
        {
            scene.SceneModel.Character.Recycle();
            scene.SceneModel.Character = null;
        }
        if(current == UIType.Character)
        {
            scene.SceneModel.Character.Recycle();
            var templateId = CharacterManager.Instance.SelectedCharacterData.TemplateId;
            scene.SceneModel.Character = ResourceManager.Instance.LoadCharcterAsset(templateId);
            scene.CharacterUI(true);
        }
        else if(current == UIType.Battle)
        {
            scene.CharacterUI(false);
        }
        
    }
}