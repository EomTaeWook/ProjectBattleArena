using Assets.Scripts.Internal;
using Kosher.Log;

public class Loading : UIComponent
{
    public static Loading Instantiate()
    {
        var prefab = ResourceManager.Instance.LoadAsset<Loading>($"Prefabs/UI/Loading");

        if (prefab == null)
        {
            LogHelper.Error($"no prefab");
            return null;
        }
        var item = UIManager.Instance.AddPopupUI(prefab);
        return item.GetComponent<Loading>();
    }
}
