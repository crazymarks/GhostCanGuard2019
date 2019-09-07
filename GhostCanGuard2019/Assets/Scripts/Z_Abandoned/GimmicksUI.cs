using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class GimmicksUI : MonoBehaviour
{
    // playerとの距離が短いときに発動するギミックはtrue
    //[SerializeField]
    //private bool toPlayerDistance = false;
    
    //private EventTrigger eventTrigger;

    //[SerializeField]
    //private GimickOpen gimickOpen;

    //private Image gimmickImage;

    //// UIのアルファ
    //float imageA = 0.4f;
    //float baseImageA;

    //private void Start()
    //{
    //    gimmickImage = this.GetComponent<Image>();
    //    baseImageA = gimmickImage.color.a;

    //    eventTrigger = this.gameObject.GetComponent<EventTrigger>();

    //    // マウスがオブジェクトに乗った時のイベント作成
    //    var entryEnter = new EventTrigger.Entry();
    //    entryEnter.eventID = EventTriggerType.PointerEnter;
    //    entryEnter.callback.AddListener((data) => SelectObjAlpha(data));
    //    this.eventTrigger.triggers.Add(entryEnter);

    //    // マウスがオブジェクトから離れた時のイベント作成
    //    var entryOut = new EventTrigger.Entry();
    //    entryOut.eventID = EventTriggerType.PointerExit;
    //    entryOut.callback.AddListener((data) => BaseObjAlpha(data));
    //    this.eventTrigger.triggers.Add(entryOut);
    //}

    ///// <summary>
    ///// Gimmickの効果発動
    ///// とりあえず今はGimmickの名前をDebugで表示
    ///// </summary>
    //public void GimmickStart()
    //{
    //    if ( !this.gameObject.activeSelf ) return;
    //    // gimmickのUIを閉じる
    //    gimickOpen.GimmickUIsOnOff(false);
    //    Debug.Log(this.gameObject.name);
    //}

    //private void SelectObjAlpha(BaseEventData data)
    //{
    //    this.GetComponent<Image>().color = new Color(0, 0, 0, imageA);
    //}

    //private void BaseObjAlpha(BaseEventData data)
    //{
    //    gimmickImage.color = new Color(0, 0, 0, baseImageA);
    //}
}
