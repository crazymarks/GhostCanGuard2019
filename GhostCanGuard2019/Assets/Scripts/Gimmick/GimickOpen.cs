using UnityEngine;
using UnityEngine.EventSystems;

/* 
 ** 外部スクリプトで呼ぶクラス
 ** ギミックにマウスクリックが起きた時に
 ** ギミック効果をUIとして展開する関数をもつ 
 ** ギミックにアタッチ
 */

public class GimickOpen : MonoBehaviour
{
    // UIの親の空オブジェ
    private GameObject gimmickUIParent;

    private EventTrigger eventTrigger;

    [SerializeField]
    private PlayerMove playerMove;

    private void Start()
    {
        eventTrigger = this.gameObject.GetComponent<EventTrigger>();
        gimmickUIParent = this.gameObject.transform.GetChild(0).gameObject;
        Debug.Log(gimmickUIParent.name);

        var entryPDOWN = new EventTrigger.Entry();
        entryPDOWN.eventID = EventTriggerType.PointerDown;
        entryPDOWN.callback.AddListener((data) => GimmickEvent(data));
        this.eventTrigger.triggers.Add(entryPDOWN);
    }

    /// <summary>
    /// gimmickの効果を表示、非表示させる
    /// </summary>
    /// <param name="onoff">true: 展開  false: 収縮</param>
    public void GimmickUIsOnOff(bool onoff)
    {
        playerMove.IsPlayerMove = false;
        // UI表示
        gimmickUIParent.SetActive(onoff);
    }

    public void GimmickEvent(BaseEventData data)
    {
        Debug.Log("gimmick touch");
        GimmickUIsOnOff(true);
    }
}
