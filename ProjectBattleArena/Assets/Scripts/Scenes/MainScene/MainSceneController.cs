﻿using Assets.Scripts.Internal;
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
            scene.SceneModel.Character = CharacterManager.Instance.LoadCharcterResource();
        }
        scene.Refresh();
    }
}