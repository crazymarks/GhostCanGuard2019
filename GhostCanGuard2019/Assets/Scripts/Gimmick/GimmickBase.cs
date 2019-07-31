using System;
using UnityEngine;

/*
 * gimmickに継承するクラス
 * gimmickにEventを追加する関数やgimmickとカーソルが
 * 重なった時にUIを表示する関数
 */

public class GimmickBase : MonoBehaviour
{

    //ギミックの効果や説明のUIの展開
<<<<<<< HEAD
    [SerializeField] protected GameObject gimmickUIParent;
    protected stop st = null;
    // 各ギミックで必要なボタンを入れる
    //[SerializeField] protected ControllerButton[] gimmickButtons = null; 
=======
    [SerializeField]
    protected GameObject gimmickUIParent;
    protected EventTrigger eventTrigger;
    protected stop st;
    // 各ギミックで必要なボタンを入れる
    [SerializeField]
    protected ControllerButton[] gimmickButtons = null;
    private EventTrigger.Entry entry = new EventTrigger.Entry();
>>>>>>> origin/wangguanyu

    virtual protected void Start()
    {
        //st = GameManager.Instance.GetComponent<stop>();
        
        if (gimmickUIParent == null)
        {
            gimmickUIParent = transform.Find("UI").gameObject;
        }
    }
    /// <summary>
    /// gimmickの効果を表示、非表示させる
    /// </summary>
    public void GimmickUIsOnOff(bool onoff)
    {
        // UIの展開とギミック発動のフラグが異なっていたらreturn
        // if (onoff != GimmickManager.Instance.GimmickFrag) return;

        // UI表示
        gimmickUIParent.SetActive(onoff);
    }

<<<<<<< HEAD
=======
        Debug.Log("gimmick touch");
        PlayerAnimationController.Instance.SetAnimatorValue(SetPAnimator.Hold);
        GimmickUIsOnOff(true);
        st.gamestop();
    }
>>>>>>> origin/wangguanyu
    /// <summary>
    /// playerがギミックとカーソルが重なっているときにボタンを押した時に呼ぶ
    /// </summary>
    public void ClickGimmick()
    {
<<<<<<< HEAD
        GimmickManager.Instance.SetGimmickAction( () => CurrentButtonIN() );
    }
    protected void CurrentButtonIN()
    {
        // 押したときのボタンをギミック処理に送る
        PushButtonGamePad(InputManager.Instance.CurrentControllerButton);
=======
        GimmickUIsOnOff(false);
        PlayerAnimationController.Instance.CancelPlayerAnimation(SetPAnimator.Hold);
        GimmickManager.Instance.ClearGimmick();
        //PlayerManager.Instance.SetCurrentState(PlayerState.Play);
>>>>>>> origin/wangguanyu
    }
    /// <summary>
    /// switch文で押されたボタンに対する処理を各ギミックで行う
    /// </summary>
    protected virtual void PushButtonGamePad(ControllerButton controller)
    {

    }

    /// <summary>
    /// gimmickに対応していないボタンが押された時messageを表示
    /// </summary>
    protected void NotButtonPushMessage()
    {
        // textのUIを表示する
        // とりあえずDebugLog出力
        Debug.Log(
            InputManager.Instance.CurrentControllerButton.ToString()
            + "は対応していないボタン");
    }
    /// <summary>
    /// playerがギミックとカーソルが重なっているときにボタンを押した時に呼ぶ
    /// </summary>
    public void ClickGimmick()
    {
        if (Input.GetButtonDown("Send"))
        {
            GimmickManager.Instance.SetGimmickAction(CurrentButtonIN);
            Debug.Log("Click");
        }
        
    }
    protected void CurrentButtonIN()
    {
        // 押したときのボタンをギミック処理に送る
        PushButtonGamePad(InputManager.Instance.CurrentControllerButton);
        Debug.Log(InputManager.Instance.CurrentControllerButton);
    }
    /// <summary>
    /// switch文で押されたボタンに対する処理を各ギミックで行う
    /// </summary>
    protected virtual void PushButtonGamePad(ControllerButton controller)
    {

    }
}
