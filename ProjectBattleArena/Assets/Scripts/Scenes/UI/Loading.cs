using Assets.Scripts.Internal;
using Kosher.Log;
using Kosher.Unity;

public class Loading : UIComponent
{
    public static Loading Instantiate()
    {
        var prefab = KosherUnityResourceManager.Instance.LoadResouce<Loading>($"Prefabs/UI/Common/Loading");

        if (prefab == null)
        {
            LogHelper.Error($"no prefab");
            return null;
        }
        var item = UIManager.Instance.AddPopupUI(prefab);
        return item.GetComponent<Loading>();
    }
}
