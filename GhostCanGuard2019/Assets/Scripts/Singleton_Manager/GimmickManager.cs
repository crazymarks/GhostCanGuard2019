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

    private GameObject[] sceneGimmicks = null;
    private string gimmickTag = "Gimmick";

    protected override void Awake()
    {
        base.Awake();
        // Sceneに配置されているGimmickを配列に入れる
        sceneGimmicks = GameObject.FindGameObjectsWithTag("Gimmik");
    }

    /// <summary>
    /// true -> Gimmick発動なし false->ギミック発動中
    /// </summary>
    public bool GimmickFrag { get; set; } = false;
    
    /// <summary>
    /// 関数をsetする
    /// 呼び方 -> GimmickManager.Instance.SetGimmickAction( () => 関数名() );
    /// </summary>
    /// <param name="action">実行したい関数</param>
    public void SetGimmickAction(System.Action action)
    {
        GimmickFunc = action;
    }
    /// <summary>
    /// Action初期化
    /// </summary>
    public void ClearGimmick()
    {
        //PlayerManager.Instance.SetCurrentState(PlayerState.Play);
<<<<<<< HEAD

=======
        if (gimmickNames != null)
        {
            gimmickNames.ForEach(name => Debug.Log(name));
            gimmickNames.Clear();
        }
>>>>>>> origin/wangguanyu
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

    /// <summary>
    /// Scene上のGimmickのUIを展開する
    /// </summary>
    public void GimmicksOpenUI()
    {
        foreach(GameObject gimmick in sceneGimmicks)
        {
            gimmick.GetComponent<GimmickBase>().GimmickUIsOnOff(true);
        }
    }
}
