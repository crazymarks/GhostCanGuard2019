
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCamera : MonoBehaviour
{
    //追跡するターゲット
    public Transform target;  

    //キャメラの最大移動速度
    public float smothing = 5f;

    public float viewDistance = 5;

    //ターゲットからの距離
    Vector3 offset;
    void Start()
    {
        

        transform.position = target.position + new Vector3(0, viewDistance, 0);
        transform.LookAt(target.position);
        offset = transform.position - target.position;
    }


    void FixedUpdate()
    {
        //ターゲット移動した後キャメラの位置
        Vector3 targetCampos = target.position + offset;
        //lerpを使て移動を緩やかします
        transform.position = Vector3.Lerp(transform.position, targetCampos, smothing * Time.deltaTime);
    }
}
