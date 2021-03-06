﻿using UnityEngine;
/// <summary>
/// 今のカーソル位置に基づく、アウトライン出来るオブジェクトを"outline"レイヤーに移動し、戻す
/// </summary>
public class OutLineManager : MonoBehaviour
{
    public StopSystem st;
    public static OutLineManager instanceoutLineManager;
    private GameObject outlineObject = null;
    // Start is called before the first frame update
    void Start()
    {
        st = GameManager.Instance.GetComponent<StopSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        if(enabled) 
            outlinecheck();
    }

    
    void outlinecheck()   
    {
        //Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        //RaycastHit hit;
        
       
        //if(Physics.Raycast(ray, out hit) && (hit.collider.gameObject.tag == "Gimmik" || hit.collider.gameObject.tag == "Player"))       //新し アウトライン出来る オブジェクトをヒットする場合
        //{
        //    if (hit.collider.gameObject.layer == LayerMask.NameToLayer("Default"))  //ヒットされたオブジェクトはデフォルト層にいる場合、つまり、アウトライン出来る状態で
        //    {
        //        if (outlineObject == null)                                          //既に アウトラインされた オブジェクトがいない
        //        {
        //            outline(hit.collider.gameObject);
        //        }

        //        else                                                                //既に アウトラインされた オブジェクトがいる
        //        {
        //            outlineCancel();                                                //今アウトラインしているオブジェクトをキャンセルし
        //            outline(hit.collider.gameObject);
        //            return;
        //        }
        //    }
        //    else                                                                    //ヒットされたオブジェクトはデフォルト層にいない場合、つまり、アウトライン出来る状態じゃない
        //        return;
           
        //}
        //else                                                                        //何もヒットしない場合
        //{
        //    outlineCancel();
        //}
        if (st.selectedObject)       //新し アウトライン出来る オブジェクトをヒットする場合
        {
            if (st.selectedObject.layer == LayerMask.NameToLayer("Default"))  //ヒットされたオブジェクトはデフォルト層にいる場合、つまり、アウトライン出来る状態で
            {
                if (outlineObject == null)                                          //既に アウトラインされた オブジェクトがいない
                {
                    outline(st.selectedObject);
                }

                else                                                                //既に アウトラインされた オブジェクトがいる
                {
                    outlineCancel();                                                //今アウトラインしているオブジェクトをキャンセルし
                    outline(st.selectedObject);
                    return;
                }
            }
            else                                                                    //ヒットされたオブジェクトはデフォルト層にいない場合、つまり、アウトライン出来る状態じゃない
                return;

        }
        else                                                                        //何もヒットしない場合
        {
            outlineCancel();
        }

    }

    //オブジェクトをアウトラインされる
    void outline(GameObject obj)
    {
        outlineObject = obj;
        ChangeLayer(outlineObject.transform, "outline");
    }

    //オブジェクトのアウトラインをキャンセルします
    void outlineCancel()
    {
        if (outlineObject == null)
            return;
        else if (outlineObject != null)
        {
            ChangeLayer(outlineObject.transform, "Default");
            outlineObject = null;
            return;
        }
    }

    void ChangeLayer(Transform trans, string targetLayer)
    {
        if (LayerMask.NameToLayer(targetLayer) == -1)
        {
            Debug.Log("layer dosen't exist");
            return;
        }
        trans.gameObject.layer = LayerMask.NameToLayer(targetLayer);
        foreach (Transform child in trans)
        {
            ChangeLayer(child, targetLayer);
        }
    }
}

