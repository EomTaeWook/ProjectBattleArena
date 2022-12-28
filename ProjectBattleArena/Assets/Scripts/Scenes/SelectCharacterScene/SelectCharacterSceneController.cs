using Assets.Scripts.Internal;
using Assets.Scripts.Scenes;
using Kosher.Unity;
using Protocol.GameWebServerAndClient.ShareModels;
using UnityEngine;

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
        var go = LoadCharcter(characterData);

        scene.SceneModel.SelectCharacter = go;

        scene.RefreshCharacter();
    }

    private GameObject LoadCharcter(CharacterData character)
    {
        var jobType = character.Job;
        var prefab = KosherUnityResourceManager.Instance.LoadResouce<GameObject>($"Prefabs/Character/{jobType}");

        var go = KosherUnityObjectPool.Instance.Pop(prefab);

        return go;
    }
}