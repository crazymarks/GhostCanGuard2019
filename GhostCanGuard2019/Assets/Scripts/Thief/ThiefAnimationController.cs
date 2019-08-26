using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ThiefAnimator
{
    Wait = 0,
    Run = 1,
    Stun = 5,
    Steal = 10,
    Captured = 50,
    Killed = 99,
} 

public class ThiefAnimationController : MonoBehaviour
{
    Thief tf;
    Animator animator;
    private string _Thief = "ThiefControl";
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        tf = transform.parent.GetComponent<Thief>();
    }

    // Update is called once per frame
    void Update()
    {
        //if(tf.thiefState==Thief.ThiefState.PAUSE|| tf.thiefState == Thief.ThiefState.END||tf.thiefState == Thief.ThiefState.EXITED)
        //{
        //    animator.SetBool("Wait", true);
        //}
    }
    public void setWaitAnimation()
    {
        animator.SetBool("Wait", true);
    }
    public void setRunAnimation()
    {
        animator.SetBool("Wait", false);
    }
    /// <summary>
    /// thiefのAnimation
    /// </summary>
    public void SetThiefAnimation(ThiefAnimator anim)
    {
        animator.SetInteger(_Thief, (int)anim);
    }
}
