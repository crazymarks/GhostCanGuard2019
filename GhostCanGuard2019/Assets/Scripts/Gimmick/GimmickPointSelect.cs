using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class GimmickPointSelect : GimmickBase
{
    [SerializeField]
    private GameObject targetObj;

    private Material targetMaterial;

    private bool eventFlag = false;
    private int eventCount = 0;

    private Vector3 gimmickStrtPos;
    public Vector3 GimmickStrtPos { get { return gimmickStrtPos; } }

    private void Start()
    {
        targetMaterial = targetObj.GetComponent<Renderer>().material;
        GimmickEventSetUp(EventTriggerType.PointerDown, SelectGimmick);
    }
    private void Update()
    {
        EventPlaying(eventFlag);

        if(eventCount != 0)
        {
            if(Input.GetMouseButtonUp(0))
            {
                eventFlag = false;
                targetObj.SetActive(eventFlag);
                eventCount = 0;
                Debug.Log(gimmickStrtPos);
            }
        }
    }
    public override void SelectGimmick(BaseEventData data)
    {
        //base.SelectGimmick(data);  
        TargetToMousePoint(targetObj);
    }

    public override void StartGimmick()
    {
        // base.StartGimmick();
        Debug.Log(this.gameObject.name);
    }

    private void TargetToMousePoint(GameObject gameObject)
    {
        eventFlag = true;
        //if (Input.GetMouseButton(0))
        //    TargetToMousePoint(targetObj);
        //else
        //    StartGimmick();
    }

    private void EventPlaying(bool flag)
    {
        if (!flag) return;

        targetObj.SetActive(flag);
        // マウス座標取得
        Vector3 mPosition = Input.mousePosition;
        mPosition.z = 15f;
        Vector3 worldMousePosition = Camera.main.ScreenToWorldPoint(mPosition);

        targetObj.transform.position = worldMousePosition;
        // shaderにマウス座標の値を送る
        targetMaterial.SetVector("_MousePosition", worldMousePosition);
        // 外部スクリプトにgimmickの発動場所を送る
        gimmickStrtPos = worldMousePosition;
        if (eventCount != 0) return;
        eventCount++;
    }
}