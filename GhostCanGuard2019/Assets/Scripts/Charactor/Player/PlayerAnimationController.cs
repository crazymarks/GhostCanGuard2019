﻿using UnityEngine;

public enum PAnimation
{
    Wait = 0,
    Run = 1,
    Kebiyin_Capture = 50,
    Kebiyin_Kill = 99,
}

public enum GimmickAnimation
{
    None = 0,
    Hold = 1,
    Push = 2,
    Horse = 4,
    HorseRun = 5,
    Revive = 6,
    Float = 8,
}

public class PlayerAnimationController : MonoBehaviour
{
    private Animator animator = null;
    private string _Player = "PlayerControl";
    private string _Gimmick = "GimmickParam";

    // Start is called before the first frame update
    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    /// <summary>
    /// Playerの普通の行動Animaiton
    /// </summary>
    public void SetNormalAnimation(PAnimation anim)
    {
        animator.SetInteger(_Player, (int)anim);
    }
    /// <summary>
    /// playerのGimmick行動のAnimaiton
    /// </summary>
    public void SetGimmickAnimation(GimmickAnimation anim)
    {
        animator.SetInteger(_Gimmick, (int)anim);
        if(anim == GimmickAnimation.Hold)
        {
            animator.SetTrigger("Hold");
        }
    }

    public void PlayPlayerAnimation(PAnimation param)
    {
        
        switch (param)
        {
            case PAnimation.Wait:
                animator.Play(param.ToString());
                break;
            case PAnimation.Run:
                animator.Play(param.ToString());
                break;
            case PAnimation.Kebiyin_Capture:
                animator.SetTrigger("Arrest");
                break;
            case PAnimation.Kebiyin_Kill:
                animator.SetTrigger("Killed");
                break;
            default:
                break;
        }
    }
    public void SetAnimaionByName(string AnimationName)
    {
        animator.Play(AnimationName);
    }
}
