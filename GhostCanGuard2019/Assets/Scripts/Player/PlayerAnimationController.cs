using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SetPAnimator
{
    Walk,
    Run,
    Hold,
    Push,
    Reset
}

public class PlayerAnimationController : MonoBehaviour
{
    private Animator animator;
    void Start()
    {
        animator = this.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z))
            SetAnimatorValue(SetPAnimator.Walk);
        if (Input.GetKeyDown(KeyCode.X))
            SetAnimatorValue(SetPAnimator.Run);
        if (Input.GetKeyDown(KeyCode.C))
            SetAnimatorValue(SetPAnimator.Hold);
        if (Input.GetKeyDown(KeyCode.V))
            SetAnimatorValue(SetPAnimator.Push);
        if (Input.GetKeyDown(KeyCode.B))
            SetAnimatorValue(SetPAnimator.Reset);

    }

    private void SetAnimatorValue(SetPAnimator pAnimator)
    {
        switch (pAnimator)
        {
            case SetPAnimator.Walk:
                animator.SetBool("Walk", true);
                Debug.Log("a");
                break;
            case SetPAnimator.Run:
                animator.SetBool("Run", true);
                break;
            case SetPAnimator.Hold:
                animator.SetBool("Hold", true);
                break;
            case SetPAnimator.Push:
                animator.SetBool("Push", true);
                break;
            case SetPAnimator.Reset:
                animator.SetBool("Walk", false);
                animator.SetBool("Run", false);
                animator.SetBool("Hold", false);
                animator.SetBool("Push", false);
                break;
        }
    }
}
