using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent( typeof(Rigidbody) )]
public class KabinGimmick : GimmickBase
{
    private Vector3 throwPos = Vector3.zero;   // 花瓶が飛んでいく場所
    private Vector3 mousePos = Vector3.zero;   // mouseの位置

    [SerializeField] private float power = 5.0f; // 花瓶を押す力
    [SerializeField] private GameObject player = null;
    [SerializeField] private float speed = 2.0f;

    private bool kabinSetPos = false;   // 花瓶がplayerの正面にあるかどうか
    private bool playerSetRotate = false;   // playerがmouseの方向を向いているかどうか

    private Rigidbody rb = null;

    protected override void Start()
    {
        base.Start();
        GimmickEventSetUp(EventTriggerType.PointerDown, GimmickEventOpen);
        rb = GetComponent<Rigidbody>();
    }

    private void KabinGimmickSetup()
    {
        Vector3 playerPosition = player.transform.position;
        playerPosition += player.transform.forward + player.transform.forward / 2;

        KabinToPlayer( playerPosition );
       
    }

    private void KabinGimmickAction()
    {
        KabinGimmickSetup();
        // 指定の位置に花瓶があるか
        if (!kabinSetPos) return;
        mousePos = MousePosition();
        mousePos.y = 0;
        player.transform.LookAt(mousePos);
        // どちらか片方でもfalseならreturn
        if (!Input.GetMouseButtonDown(0) || !playerSetRotate) return;
        throwPos = mousePos;
        throwPos.y = 2;

        // animation change
        GimmickManager.Instance.PlayerPushAnimation();
        // 力を加える方向をきめる
        Vector3 direction = (throwPos - this.transform.position).normalized;
        // Debug.Log(direction); Debug.Log("thorw!");
        rb.AddForce(direction * power);
        throwPos = Vector3.zero;

        GimmickManager.Instance.ClearGimmick();
        GimmickUIClose();

    }

    // ButtonのonClickで呼ぶ関数
    public void ClickUIStart()
    {
        GimmickManager.Instance.SetGimmickAction(KabinGimmickAction);
        GimmickUIsOnOff(false);
    }

    /// <summary>
    /// Playerの正面まで動かす処理
    /// </summary>
    private void KabinToPlayer(Vector3 pPos)
    {
        transform.position = pPos;
        //// playerの正面に動く
        //while(transform.position != pPos)
        //{
        //    transform.position = Vector3.MoveTowards(transform.position, pPos, speed);
        //}
        // playerの子に設定する
        // playerが回転した時に追従するため
        this.transform.parent = player.transform;
        kabinSetPos = true;
    }
    /// <summary>
    /// mouseの向きを向く
    /// </summary>
    /// <returns> playerの正面とplayer、mouse二点間のベクトルの内積 </returns>
    private float PlayerLookatMouse()
    {
        mousePos = MousePosition();
        // player -> mouse のベクトル
        Vector3 playerToMouse = (player.transform.position - mousePos).normalized;
        // playerの正面とplayer、mouse二点間のベクトルの内積
        float angle = Vector3.Dot(player.transform.forward, playerToMouse);

        // player回転
        player.transform.Rotate(Vector3.up, angle);

        return angle;
    }

    /// <summary>
    /// mouseの位置を返す
    /// </summary>
    private Vector3 MousePosition()
    {
        mousePos = Input.mousePosition;
        mousePos.z = 10f;
        mousePos = Camera.main.ScreenToWorldPoint(mousePos);

        return mousePos;
    }
}
