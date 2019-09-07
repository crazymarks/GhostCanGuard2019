using System;
using UnityEngine;

/*
 * gimmickに継承するクラス
 * gimmickにEventを追加する関数やgimmickクリック時にUIを表示する関数
 */

public class GimmickBase : MonoBehaviour
{

    //ギミックの効果や説明のUIの展開
    [SerializeField]
    protected GameObject gimmickUIParent;
    protected StopSystem st;
    // 各ギミックで必要なボタンを入れる
    [SerializeField]
    protected ControllerButton[] gimmickButtons = null;

    protected bool descriptionUIOn = false; //説明文が展開されているかのフラグ　//オウカンウ

    protected void _start()
    {
        st = GameManager.Instance.GetComponent<StopSystem>();
        
        if (gimmickUIParent == null)
        {
            gimmickUIParent = transform.Find("ButtonUICanvas").gameObject;
        }
    }
    /// <summary>
    /// gimmickの効果を表示、非表示させる
    /// </summary>
    /// <param name="onoff">true: 展開  false: 収縮</param>
    public void GimmickUIsOnOff(bool onoff)
    {
        // UIの展開とギミック発動のフラグが異なっていたらreturn
        if (onoff != GimmickManager.Instance.GimmickFrag) return;
        
        // playerの動きを止める
        //PlayerManager.Instance.SetCurrentState(PlayerState.Gimmick);
        // UI表示
        gimmickUIParent.SetActive(onoff);
        // Gimmickが発動待ち
        GimmickManager.Instance.GimmickFrag = false;
    }
    /// <summary>
    /// gimmickの選択を解除する
    /// </summary>
    public void GimmickUIClose()
    {
        GimmickUIsOnOff(false);
        PlayerAnimationController.Instance.CancelPlayerAnimation(SetPAnimator.Hold);
        GimmickManager.Instance.ClearGimmick();
        //PlayerManager.Instance.SetCurrentState(PlayerState.Play);
    }
    /// <summary>
    /// playerがギミックとカーソルが重なっているときにボタンを押した時に呼ぶ
    /// </summary>
    public void ClickGimmick()
    {
        if (Input.GetButtonDown("Send") || Input.GetButtonDown("Info"))  //前押し処理お避けるため加えた制限//オウカンウ
        {
            GimmickManager.Instance.SetGimmickAction(() => CurrentButtonIN());      
        }
           
        //Debug.Log("Click");
    }
    protected void CurrentButtonIN()
    {
        if (st.IfSystemPause) return;
        // 押したときのボタンをギミック処理に送る
        PushButtonGamePad(InputManager.Instance.CurrentControllerButton);
        InputManager.Instance.ClearCurrentButton();
        GimmickUIsOnOff(false);
        //Debug.Log(InputManager.Instance.CurrentControllerButton);
    }
    /// <summary>
    /// switch文で押されたボタンに対する処理を各ギミックで行う
    /// </summary>
    protected virtual void PushButtonGamePad(ControllerButton controller)
    {

    }


    /// <summary>
    /// 説明文UIの展開と収縮関数です　//オウカンウ
    /// </summary>
    /// <param name="ギミックのCSVファイル中の名前"></param>
    protected void ShowDescription(string name)
    {
        if (descriptionUIOn ||st.SecondPhase) return;
        LoadDescription.Instance.ShowDesc(name);
        descriptionUIOn = true;
        st.gamestop(StopSystem.PauseState.DescriptionOpen);
        gimmickUIParent.GetComponent<DescriptionUIChange>().DescriptionOnOff();
        
    }

    protected void HideDescription()
    {
        if (!descriptionUIOn) return;
        LoadDescription.Instance.HideDesc();
        descriptionUIOn = false;
        st.gamestop(StopSystem.PauseState.DescriptionClose);
        gimmickUIParent.GetComponent<DescriptionUIChange>().DescriptionOnOff();
        GimmickManager.Instance.ClearGimmick();                 //収縮の時はギミックの登録をクリアします
    }


}
