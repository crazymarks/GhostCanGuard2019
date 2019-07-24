using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
/*----------------------------------------------------------------
 * GimmickManager
 * ギミックごとに定められている関数をAction型のGimmickFuncに登録し
 * GimmickFuncをタイミングごとに呼び出す。
 ----------------------------------------------------------------*/


public class GimmickManager : SingletonMonoBehavior<GimmickManager>
{
    // gimmickそれぞれにつける
    private System.Action GimmickFunc = null;

    private List<string> gimmickNames = new List<string>();

    /// <summary>
    /// true -> Gimmick発動なし false->ギミック発動中
    /// </summary>
    public bool GimmickFrag { get; set; }


    private void Start()
    {
        GimmickFrag = false;
    }

    /// <summary>
    /// 関数をsetする
    /// 呼び方 -> GimmickManager.Instance.SetGimmickAction( () => 関数名() );
    /// </summary>
    /// <param name="action">実行したい関数</param>
    public void SetGimmickAction(System.Action action)
    {
        GimmickFunc = action;
        gimmickNames.Add(action.Method.Name);
    }
    /// <summary>
    /// Action初期化
    /// </summary>
    public void ClearGimmick()
    {
        //PlayerManager.Instance.SetCurrentState(PlayerState.Play);
        if (gimmickNames != null)
        {
            gimmickNames.ForEach(name => Debug.Log(name));
            gimmickNames.Clear();
        }
        GimmickFunc = null;
        GimmickFrag = true;
    }

    public void PlayerPushAnimation()
    {
        PlayerAnimationController.Instance.SetAnimatorValue(SetPAnimator.Push);
    }

    private void Update()
    {
        if (GimmickFunc == null) return;
        GimmickFunc();
    }

}
