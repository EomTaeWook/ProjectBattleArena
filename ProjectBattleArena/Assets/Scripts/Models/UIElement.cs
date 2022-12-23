using Assets.Scripts.Internal;
using UnityEngine;

public class UIComponent : MonoBehaviour
{
    public virtual void Dispose()
    {
        UIManager.Instance.CloseUI(this);
    }
}