using Assets.Scripts.Internal;
using Kosher.Log;
using Kosher.Unity;
using UnityEngine;

public class MainCharacterUI : UIComponent
{
    public static MainCharacterUI Instantiate(Transform parent)
    {
        var prefab = ResourceManager.Instance.LoadAsset<MainCharacterUI>($"Prefabs/Lobby/LobbyCharacterUI");

        if (prefab == null)
        {
            LogHelper.Error($"no prefab");
            return null;
        }

        var item = UIManager.Instance.AddUI(prefab, parent);
        return item.GetComponent<MainCharacterUI>();
    }




}