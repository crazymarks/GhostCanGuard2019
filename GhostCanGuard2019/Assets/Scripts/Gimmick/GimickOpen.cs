using UnityEngine;
using UnityEngine.EventSystems;

/* 
 ** 外部スクリプトで呼ぶクラス
 ** ギミックにマウスクリックが起きた時に
 ** ギミック効果をUIとして展開する関数をもつ 
 ** ギミックにアタッチ
 */

public class GimickOpen : GimmickBase
{

    private void Start()
    {
        GimmickEventSetUp(EventTriggerType.PointerDown, GimmickEventOpen);
        GimmickEventSetUp(EventTriggerType.PointerDown, SelectGimmick);
        gimmickUIParent = this.gameObject.transform.GetChild(0).gameObject;
    }


    private void DebugFunction(BaseEventData data)
    {
        Debug.Log("正常に動いてます");
    }

    public override void SelectGimmick(BaseEventData data)
    {
        Vector3 mPosition = Input.mousePosition;
        mPosition.z = 10f;

        Vector3 worldMousePosition = Camera.main.ScreenToWorldPoint(mPosition);
        worldMousePosition.x += 2;

        //Debug.Log(this.transform.position);
        //Debug.Log(worldMousePosition);

        if (worldMousePosition.x <= this.transform.position.x)
        {
            // キャンセルする
            GimmickUIClose();
            return;
        }
        else
        {
            // gimmickを作動させる
            StartGimmick();
        }        
    }
}
