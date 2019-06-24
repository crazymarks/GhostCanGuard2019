using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class GimmickPointSelect : GimmickBase
{
    //[SerializeField]
    //private GameObject targetObj;

    //private Material targetMaterial;
    
    //private Vector3 gimmickStrtPos;
    //public Vector3 GimmickStrtPos => gimmickStrtPos;

    ////private void Start()
    ////{
    ////    targetMaterial = targetObj.GetComponent<Renderer>().material;
    ////    //GimmickEventSetUp(EventTriggerType.PointerDown, SelectGimmick);
    ////}
    //private void Update()
    //{
    //    //// EventPlaying();
    //    //if (!GimmickBase.GimmickFlag) return;
    //    //OnButttonStateChange(ButtonManager.Instance.CurrentButton);
    //}
    ////public override void SelectGimmick(BaseEventData data)
    ////{
    ////    //base.SelectGimmick(data);  
    ////    TargetToMousePoint(targetObj);
    ////    GimmickBase.GimmickFlag = true;
    ////}

    ////public override void StartGimmick()
    ////{
    ////    // base.StartGimmick();
    ////    Debug.Log(this.gameObject.name);
    ////}

    //private void TargetToMousePoint(GameObject gameObject)
    //{
    //    //if (Input.GetMouseButton(0))
    //    //    TargetToMousePoint(targetObj);
    //    //else
    //    //    StartGimmick();
    //}

    //private void EventPlaying()
    //{
    //    targetObj.SetActive(true);
    //    // マウス座標取得
    //    Vector3 mPosition = Input.mousePosition;
    //    mPosition.z = 15f;
    //    Vector3 worldMousePosition = Camera.main.ScreenToWorldPoint(mPosition);

    //    targetObj.transform.position = worldMousePosition;
    //    // shaderにマウス座標の値を送る
    //    targetMaterial.SetVector("_MousePosition", worldMousePosition);
    //    // 外部スクリプトにgimmickの発動場所を送る
    //    gimmickStrtPos = worldMousePosition;
    //}
    //private void OnButttonStateChange(ButtonState state)
    //{
    //    switch (state)
    //    {
    //        case ButtonState.None:
    //            break;
    //        case ButtonState.Stay:
    //            EventPlaying();
    //            break;
    //        case ButtonState.Release:
    //            targetObj.SetActive(false);
    //            Debug.Log(targetObj.transform.position);
    //            //GimmickBase.GimmickFlag = false;
    //            ButtonManager.Instance.ChangeButtonState(ButtonState.None);
    //            break;
    //    }
    //}
}