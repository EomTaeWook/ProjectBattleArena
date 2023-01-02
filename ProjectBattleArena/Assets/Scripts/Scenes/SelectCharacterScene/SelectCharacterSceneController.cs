using Assets.Scripts.Internal;
using Assets.Scripts.Scenes;
using Kosher.Unity;
using Protocol.GameWebServerAndClient.ShareModels;

internal class SelectCharacterSceneController : SceneController<SelectCharacterSceneController>
{
    SelectCharacterScene scene;
    public override void BindScene(BaseScene baseScene)
    {
        scene = baseScene as SelectCharacterScene;
    }
    public void Init()
    {
        for (int i = 0; i < CharacterManager.Instance.GetCharacterDatas().Count; ++i)
        {
            var item = CharacterManager.Instance.GetCharacterDatas()[i];
            if (i == 0)
            {
                SelectCharacter(item);
            }
            var slotItem = CharacterSlot.Instantiate();
            slotItem.Init(item);
            scene.SceneModel.Slots.Add(slotItem);
        }

        var createdSlotItem = CharacterSlot.Instantiate();
        createdSlotItem.SetCreateMode(true);
        scene.SceneModel.Slots.Add(createdSlotItem);

        scene.Refresh();
    }
    public void Dispose()
    {
        foreach (var item in this.scene.SceneModel.Slots)
        {
            item.Recycle<CharacterSlot>();
        }
        scene.SceneModel.Slots.Clear();

        if (scene.SceneModel.SelectCharacter != null)
        {
            scene.SceneModel.SelectCharacter.Recycle();
        }
    }
    public void SelectCharacter(CharacterData characterData)
    {
        if (scene.SceneModel.SelectCharacter != null)
        {
            scene.SceneModel.SelectCharacter.Recycle();
        }
        CharacterManager.Instance.SetSelectedCharacterData(characterData);

        var go = ResourceManager.Instance.LoadCharcterAsset(characterData.TemplateId);

        scene.SceneModel.SelectCharacter = go;

        scene.RefreshCharacter();
    }
}