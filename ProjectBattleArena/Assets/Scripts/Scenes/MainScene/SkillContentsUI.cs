using Kosher.Log;
using Kosher.Unity;
using UnityEngine.UI;

public class SkillContentsUI : UIComponent
{
    public static SkillContentsUI Instantiate()
    {
        var prefab = KosherUnityResourceManager.Instance.LoadResouce<SkillContentsUI>($"Prefabs/Lobby/SkillContentsUI");

        if (prefab == null)
        {
            LogHelper.Error($"no prefab");
            return null;
        }

        var item = KosherUnityObjectPool.Instance.Pop<SkillContentsUI>(prefab);
        return item;
    }

    public void OnSelectSkillClick(int index)
    {

    }

    public void OnCloseClick()
    {
        this.Dispose();
    }

}