using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ThiefAnimator
{
    Wait = 0, // ○
    Run = 1, // ○
    Stun = 5,
    Steal = 10, // ○
    dorobo_Capture = 50,
    dorobo_Kill = 99,
}

public class ThiefAnimationController : MonoBehaviour
{
    Animator animator;
    private string _Thief = "ThiefControl";
    // Start is called before the first frame update
    void Awake()
    {
        animator = GetComponent<Animator>();
    }
    /// <summary>
    /// thiefのAnimation
    /// </summary>
    public void SetThiefAnimation(ThiefAnimator anim)
    {
        animator.SetInteger(_Thief, (int)anim);
    }
    /// <summary>
    /// dethとCaptureをPlayさせる関数
    /// </summary>
    /// <param name="thiefParam"></param>
    public void ThiefAnimatorPlay(ThiefAnimator thiefParam)
    {
        if ((int)thiefParam < 11) return;
        animator.Play(thiefParam.ToString());
    }
}