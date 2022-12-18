using UnityEngine;

public abstract class BaseScene : MonoBehaviour
{
    private void OnDestroy()
    {
        OnDestroyScene();
    }

    private void Awake()
    {
        OnAwakeScene();
    }

    private void OnEnable()
    {
        OnShow();
    }

    private void OnDisable()
    {
        OnHide();
    }

    public abstract void OnAwakeScene();
    public abstract void OnDestroyScene();

    public virtual void OnHide() 
    {
    }
    public virtual void OnShow() 
    {
    }
}
