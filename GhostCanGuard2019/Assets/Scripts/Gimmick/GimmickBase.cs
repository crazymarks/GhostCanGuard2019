using System;
using UnityEngine;
using UnityEngine.EventSystems;

/*
 * gimmickに継承するクラス
 * gimmickにEventを追加する関数やgimmickクリック時にUIを表示する関数
 */

public class GimmickBase : MonoBehaviour
{

    //ギミックの効果や説明のUIの展開
    [SerializeField]
    protected GameObject gimmickUIParent;
    protected EventTrigger eventTrigger;
    protected stop st;
    // 各ギミックで必要なボタンを入れる
    [SerializeField]
    protected ControllerButton[] gimmickButtons = null;
    private EventTrigger.Entry entry = new EventTrigger.Entry();

    protected bool descriptionUIOn = false; //説明文が展開されているかのフラグ　//オウカンウ

    // public static bool GimmickFlag = false;

    //virtual protected void Start()
    //{
    //    eventTrigger = this.gameObject.GetComponent<EventTrigger>();
    //    gimmickUIParent = this.transform.GetChild(0).gameObject;
    //}
    virtual protected void Start()
    {
        st = GameManager.Instance.GetComponent<stop>();
        eventTrigger = this.gameObject.GetComponent<EventTrigger>();
        
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
    // Eventに追加される関数
    public void GimmickEventOpen(BaseEventData data)
    {

        Debug.Log("gimmick touch");
        PlayerAnimationController.Instance.SetAnimatorValue(SetPAnimator.Hold);
        GimmickUIsOnOff(true);
        //st.gamestop();
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
    /// gimmickのイベント作成関数
    /// </summary>
    /// <param name="triggerType">EventtTrriget.type</param>
    /// <param name="action">BaseEventDataが引数のvoid関数</param>
    protected void GimmickEventSetUp(EventTriggerType triggerType, Action<BaseEventData> action)
    {
        // eventを作成し、Triggerに追加する
        entry.eventID = triggerType;
        entry.callback.AddListener((data) => action(data));
        eventTrigger.triggers.Add(entry);
    }

    /// <summary>
    /// Gimmickのイベントを削除する
    /// </summary>
    protected void ClearGimmickEvent()
    {
        if (this.eventTrigger.triggers == null) return;

        this.eventTrigger.triggers.Remove(entry);
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
        if (descriptionUIOn) return;
        LoadDescription.Instance.ShowDesc(name);
        descriptionUIOn = true;
        st.gamestop(stop.PauseState.DescriptionOpen);
        gimmickUIParent.GetComponent<DescriptionUIChange>().DescriptionOnOff();
        
    }

    protected void HideDescription()
    {
        if (!descriptionUIOn) return;
        LoadDescription.Instance.HideDesc();
        descriptionUIOn = false;
        st.gamestop(stop.PauseState.DescriptionClose);
        gimmickUIParent.GetComponent<DescriptionUIChange>().DescriptionOnOff();
        GimmickManager.Instance.ClearGimmick();                 //収縮の時はギミックの登録をクリアします
    }


}
