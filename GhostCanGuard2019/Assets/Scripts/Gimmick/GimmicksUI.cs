using UnityEngine;
using UnityEngine.EventSystems;

public class GimmicksUI : MonoBehaviour
{
    // playerとの距離が短いときに発動するギミックはtrue
    //[SerializeField]
    //private bool toPlayerDistance = false;

    [SerializeField]
    private EventTrigger eventTrigger;

    private void Start()
    {
        /// マウスのポインターを話した時のイベント作成
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
        Debug.Log(this.gameObject.name);
    }
}
