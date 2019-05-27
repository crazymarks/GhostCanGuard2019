using System;
using UnityEngine;
using UnityEngine.EventSystems;

/*
 * gimmickに継承するクラス
 * gimmickにEventを追加する関数やgimmick選択時にUIを表示する関数
 */

public class GimmickBase : MonoBehaviour
{
    protected GameObject gimmickUIParent;
    
    protected EventTrigger eventTrigger;

    public static bool GimmickFlag = false;

    /// <summary>
    /// gimmickの効果を表示、非表示させる
    /// </summary>
    /// <param name="onoff">true: 展開  false: 収縮</param>
    public void GimmickUIsOnOff(bool onoff)
    {
        // playerの動きを止める
        PlayerManager.Instance.SetCurrentState(PlayerState.Stop);
        // UI表示
        gimmickUIParent.SetActive(onoff);
    }

    // Eventに追加される関数
    public void GimmickEventOpen(BaseEventData data)
    {
        Debug.Log("gimmick touch");
        GimmickUIsOnOff(true);
    }
    /// <summary>
    /// gimmickの選択を解除する
    /// </summary>
    public void GimmickUIClose()
    {
        GimmickUIsOnOff(false);
    }
    /// <summary>
    /// Gimmick選択
    /// 地点選択と効果選択
    /// </summary>
    public virtual void SelectGimmick(BaseEventData data)
    {
        // 継承先でoverrideする
    }
    /// <summary>
    /// gimmickの効果開始
    /// </summary>
    public virtual void StartGimmick()
    {
        // 継承先でoverrideする
    }

    /// <summary>
    /// gimmickのイベント作成関数
    /// </summary>
    /// <param name="triggerType">EventtTrriget.type</param>
    /// <param name="action">BaseEventDataが引数のvoid関数</param>
    public void GimmickEventSetUp(EventTriggerType triggerType, Action<BaseEventData> action)
    {
        // ギミックのイベント
        eventTrigger = this.gameObject.GetComponent<EventTrigger>();
        // UIの親の空オブジェ
        // gimmickUIParent = this.gameObject.transform.GetChild(0).gameObject;

        // eventを作成し、Triggerに追加する
        var entry = new EventTrigger.Entry();
        entry.eventID = triggerType;
        entry.callback.AddListener((data) => action(data));
        this.eventTrigger.triggers.Add(entry);
    }
    
    //public virtual void OnButttonStateChange(ButtonState state)
    //{
    //    switch(state)
    //    {
    //        case ButtonState.None:
    //            break;
    //        case ButtonState.Stay:
    //            break;
    //        case ButtonState.Release:
    //            break;
    //    }
    //}
}
