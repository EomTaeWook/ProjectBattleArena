using Assets.Scripts.Internal;
using UnityEngine;

public class UIComponent : MonoBehaviour
{
    public virtual void DisposeUI()
    {
        UIManager.Instance.CloseUI(this);
    }
}