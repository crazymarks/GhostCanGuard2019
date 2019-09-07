using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GhostAnimator
{
    Walk = 0,
    Down = 1,
    StandUp = 2,
    Stun = 5,
    Kill = 99,
}

public class GhostAnimationController : MonoBehaviour
{
    private Animator animator = null;
    private string _Ghost = "GhostControl";

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void SetGhostAnimation(GhostAnimator anim)
    {
        animator.SetInteger(_Ghost, (int)anim);
    }
    public void ResetGhostAnimation()
    {
        animator.SetInteger(_Ghost, (int)GhostAnimator.Walk);
    }
}