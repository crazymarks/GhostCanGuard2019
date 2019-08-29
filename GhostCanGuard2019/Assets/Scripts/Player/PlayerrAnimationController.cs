using UnityEngine;

public enum PAnimation
{
    Wait = 0,
    Run = 1,
    Capture = 50,
    Killed = 99,
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

public class PlayerrAnimationController : SingletonMonoBehavior<PlayerrAnimationController>
{
    private Animator animator = null;
    private string _Player = "PlayerControl";
    private string _Gimmick = "GimmickParam";

    // Start is called before the first frame update
    void Start()
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
    }
}
