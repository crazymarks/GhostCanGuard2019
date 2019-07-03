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

    private EventTrigger.Entry entry = new EventTrigger.Entry();

    // public static bool GimmickFlag = false;

    //virtual protected void Start()
    //{
    //    eventTrigger = this.gameObject.GetComponent<EventTrigger>();
    //    gimmickUIParent = this.transform.GetChild(0).gameObject;
    //}
    virtual protected void Start()
    {
        eventTrigger = this.gameObject.GetComponent<EventTrigger>();
        
        if (gimmickUIParent == null)
        {
            gimmickUIParent = transform.Find("UI").gameObject;
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
        PlayerManager.Instance.SetCurrentState(PlayerState.Gimmick);
        // UI表示
        gimmickUIParent.SetActive(onoff);
        // Gimmickが発動待ち
        GimmickManager.Instance.GimmickFrag = false;
    }
    // Eventに追加される関数
    public void GimmickEventOpen(BaseEventData data)
    {
        Debug.Log("gimmick touch");
        GimmickUIsOnOff(true);
        Time.timeScale = 0f;
    }
    /// <summary>
    /// gimmickの選択を解除する
    /// </summary>
    public void GimmickUIClose()
    {
        Time.timeScale = 1f;
        GimmickUIsOnOff(false);
        GimmickManager.Instance.ClearGimmick();
        PlayerManager.Instance.SetCurrentState(PlayerState.Play);
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
        this.eventTrigger.triggers.Add(entry);
    }

    /// <summary>
    /// Gimmickのイベントを削除する
    /// </summary>
    protected void ClearGimmickEvent()
    {
        if (this.eventTrigger.triggers == null) return;

        this.eventTrigger.triggers.Remove(entry);
    }
}
