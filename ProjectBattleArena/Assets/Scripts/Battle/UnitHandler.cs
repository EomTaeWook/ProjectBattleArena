using Assets.Scripts.Models;
using UnityEditor.Animations;
using UnityEngine;

public class UnitHandler : MonoBehaviour
{
    [SerializeField]
    private Animator animator;

    public void Idle()
    {
        SetTrigger(AnimationType.Idle);
    }
    public void Move()
    {
        SetTrigger(AnimationType.Move);
    }
    public void Die()
    {
        SetTrigger(AnimationType.Die);
    }
    public void Hit()
    {
        SetTrigger(AnimationType.Hit);
    }
    public void Attack1()
    {
        SetTrigger(AnimationType.Attack1);
    }
    public void Attack2()
    {
        SetTrigger(AnimationType.Attack2);
    }

    private void SetTrigger(AnimationType animationType)
    {
        foreach(var parameter in animator.parameters)
        {
            animator.SetBool(parameter.name, false);
        }

        animator.SetTrigger(animationType.ToString());
    }

} 