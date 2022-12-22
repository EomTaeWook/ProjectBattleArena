using Assets.Scripts.Internal;
using Kosher.Unity;
using UnityEngine;

public class UIElement : MonoBehaviour
{
    public void Dispose()
    {
        UIManager.Instance.CloseUI(this);
    }
}