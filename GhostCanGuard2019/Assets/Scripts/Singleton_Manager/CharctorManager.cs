using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*------------------------------------------------------------
 * charctorのManager
 * キャラクターの動きや速度を管理する
 ------------------------------------------------------------*/

public class CharctorManager : SingletonMonoBehavior<CharctorManager>
{
    private bool charctorMovement = false;
    public bool CharctorMovement { set => charctorMovement = value; }


    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (charctorMovement) return;
        // 一時停止時の動き
        
    }
}
