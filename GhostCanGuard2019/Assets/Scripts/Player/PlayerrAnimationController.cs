using UnityEngine;

public enum PAnimation
{
    Wait = 0,
    Run = 1,
    Hold = 10,
    Capture = 50,
    Killed = 99,
}

public enum GimmickAnimation
{
    None = 0,
    Push = 2,
    Horse = 4,
    Revive = 6,
    Float = 8,
}

public class PlayerrAnimationController : MonoBehaviour
{
    private Animator animator = null;
    private string _Player = "PlayerControl";
    private string _Gimmick = "GimmickParam";

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

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
