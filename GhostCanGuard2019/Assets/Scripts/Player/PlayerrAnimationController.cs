using System.Collections;
using System.Collections.Generic;
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
    Push = 2,
    Float = 4,
    Revive = 6,
    Horse = 8,
    Killed = 99,
}

public class PlayerrAnimationController : MonoBehaviour
{
    private Animator animator = null;
    [SerializeField] private Resources gimmickAnimController = null;   // Gimmick発動時のアニメーション
    private Resources playerAnimController = null;

    // Start is called before the first frame update
    void Start()
    {
        //playerAnimController = animator.runtimeAnimatorController
    }

    // Update is called once per frame
    void Update()
    {
        // https://qiita.com/hakuta/items/7208576a1af399dc8a65
        // http://tsubakit1.hateblo.jp/entry/2016/02/11/021743
    }
}
