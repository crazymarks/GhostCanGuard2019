using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public enum SetPAnimator
{
    Walk,
    Run,
    Hold,
    Push,
    Stop
}

public class PlayerAnimationController : SingletonMonoBehavior<PlayerAnimationController>
{
    private Animator animator = null;
    private List<SetPAnimator> pAnimatorsList = new List<SetPAnimator>();
    private SetPAnimator pastAnimator = SetPAnimator.Stop;


    void Start()
    {
        // animator取得
        animator = this.GetComponent<Animator>();
        // 列挙型SetPAnimatorをList化
        pAnimatorsList = Enum.GetValues(typeof(SetPAnimator)).Cast<SetPAnimator>().ToList();
    }

    // Update is called once per frame
    void Update()
    {
        //Debug用
        //if (Input.GetKeyDown(KeyCode.Z))
        //    SetAnimatorValue(SetPAnimator.Walk);
        //if (Input.GetKeyDown(KeyCode.X))
        //    SetAnimatorValue(SetPAnimator.Run);
        //if (Input.GetKeyDown(KeyCode.C))
        //    SetAnimatorValue(SetPAnimator.Hold);
        //if (Input.GetKeyDown(KeyCode.V))
        //    SetAnimatorValue(SetPAnimator.Push);
        //if (Input.GetKeyDown(KeyCode.B))
        //    SetAnimatorValue(SetPAnimator.Stop);
        

    }
    /// <summary>
    /// 指定されたAnimatorの値以外の変数を初期化(falseする 
    /// </summary>
    public void SetAnimatorValue(SetPAnimator param)
    {
        if(pastAnimator == SetPAnimator.Hold)
        {
            if (param != SetPAnimator.Push && param != SetPAnimator.Hold) return;
        }
        // animatorで遷移したい動きを先頭に持ってくる。
        if (pAnimatorsList.IndexOf(param) > 0)
        {
            pAnimatorsList.RemoveAt(pAnimatorsList.IndexOf(param));
            pAnimatorsList.Insert(0, param);
        }
        // 遷移したい動きにする。
        animator.SetBool(param.ToString(), true);

        // そのほかの動きをfalseにする
        for(int i = 1; i < pAnimatorsList.Count; i++)
        {
            animator.SetBool(pAnimatorsList[i].ToString(), false);
        }
        pastAnimator = param;
    }
    /// <summary>
    /// falseにする処理
    /// </summary>
    public void CancelPlayerAnimation(SetPAnimator param)
    {
        animator.SetBool(param.ToString(), false);
        pastAnimator = SetPAnimator.Stop;
    }
}
