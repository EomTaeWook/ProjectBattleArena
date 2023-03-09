using Assets.Scripts.Internal;
using UnityEngine;

public class UIItem : MonoBehaviour
{
    public virtual void DisposeUI()
    {
        UIManager.Instance.CloseUI(this);
    }
}