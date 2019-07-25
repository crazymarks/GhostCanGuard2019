
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class MainCamera : MonoBehaviour
{

    //追跡するターゲット
    public Transform target;  

    //カメラの最大移動速度
    public float smothing = 5f;

    //カメラからの距離
    [Range(0,30)]
    public float distance=10;
    //カメラの縦角度
    [Range(0, 90)]
    public float Angle=80;

    //ターゲットからの距離
    Vector3 offset;
    void Start()
    {
        transform.position = target.position + new Vector3(0, distance, 0);
       
        offset = transform.position - target.position;
    }


    void FixedUpdate()
    {
        //ターゲット移動した後カメラの位置
        Vector3 targetCampos = target.position + offset;
        //lerpを使て移動を緩やかします
        transform.position = Vector3.Lerp(transform.position, targetCampos, smothing * Time.deltaTime);
        
    }
}
