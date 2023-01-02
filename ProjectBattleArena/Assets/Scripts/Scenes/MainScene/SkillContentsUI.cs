using Assets.Scripts.Internal;
using Kosher.Log;
using Kosher.Unity;

public class SkillContentsUI : UIComponent
{
    public static SkillContentsUI Instantiate()
    {
        var prefab = ResourceManager.Instance.LoadAsset<SkillContentsUI>($"Prefabs/Lobby/SkillContentsUI");

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