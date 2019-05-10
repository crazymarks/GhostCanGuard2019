using UnityEngine;
using UnityEngine.EventSystems;

public class GimmicksUI : MonoBehaviour
{
    // playerとの距離が短いときに発動するギミックはtrue
    //[SerializeField]
    //private bool toPlayerDistance = false;
    
    private EventTrigger eventTrigger;

    [SerializeField]
    GimickOpen gimickOpen;

    private void Start()
    {
        eventTrigger = this.gameObject.GetComponent<EventTrigger>();

        /// マウスのポインターを離した時のイベント作成
        var entryPUP = new EventTrigger.Entry();
        entryPUP.eventID = EventTriggerType.PointerUp;
        entryPUP.callback.AddListener((data) => GimmickStart(data));
        this.eventTrigger.triggers.Add(entryPUP);
    }

    /// <summary>
    /// Gimmickの効果発動
    /// とりあえず今はGimmickの名前をDebugで表示
    /// </summary>
    private void GimmickStart(BaseEventData data)
    {
        if ( !this.gameObject.activeSelf ) return;
        // gimmickのUIを閉じる
        gimickOpen.GimmickUIsOnOff(false);
        Debug.Log(this.gameObject.name);
    }
}
