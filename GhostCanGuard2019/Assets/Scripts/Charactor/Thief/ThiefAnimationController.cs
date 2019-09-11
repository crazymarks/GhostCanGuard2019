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
[RequireComponent(typeof(Animator))]
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
        switch (anim)
        {
            case ThiefAnimator.Stun:
                animator.SetTrigger("Stun");
                break;
            case ThiefAnimator.dorobo_Capture:
                animator.SetTrigger("Arrested");
                break;
            case ThiefAnimator.dorobo_Kill:
                animator.SetTrigger("Killed");
                break;
            default:
                break;
        }
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

    public void SetAnimationbyName(string AnimationName)
    {
        animator.Play(AnimationName);
    }
}