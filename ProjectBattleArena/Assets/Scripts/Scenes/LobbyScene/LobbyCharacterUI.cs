using Assets.Scripts.Internal;
using Kosher.Log;
using Kosher.Unity;
using UnityEngine;

public class LobbyCharacterUI : UIComponent
{
    public static LobbyCharacterUI Instantiate(Transform parent)
    {
        var prefab = KosherUnityResourceManager.Instance.LoadResouce<LobbyCharacterUI>($"Prefabs/Lobby/LobbyCharacterUI");

        if (prefab == null)
        {
            LogHelper.Error($"no prefab");
            return null;
        }

        var item = UIManager.Instance.AddUI(prefab, parent);
        return item.GetComponent<LobbyCharacterUI>();
    }




}