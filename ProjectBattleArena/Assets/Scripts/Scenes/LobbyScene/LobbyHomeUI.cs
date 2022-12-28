using Assets.Scripts.Internal;
using Kosher.Log;
using Kosher.Unity;
using UnityEngine;

public class LobbyHomeUI : UIComponent
{
    public static LobbyHomeUI Instantiate(Transform parent)
    {
        var prefab = KosherUnityResourceManager.Instance.LoadResouce<LobbyHomeUI>($"Prefabs/Lobby/LobbyHomeUI");

        if (prefab == null)
        {
            LogHelper.Error($"no prefab");
            return null;
        }

        var item = UIManager.Instance.AddUI(prefab, parent);
        return item.GetComponent<LobbyHomeUI>();
    }
}