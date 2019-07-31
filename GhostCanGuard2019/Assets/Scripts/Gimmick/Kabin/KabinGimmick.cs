using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class KabinGimmick : GimmickBase
{
    private Vector3 throwPos = Vector3.zero;   // 花瓶が飛んでいく場所

    [SerializeField] private float power = 5.0f; // 花瓶を押す力
    private GameObject player = null;

    [SerializeField] private float speed = 2.0f;    // speed when kabin move to player
    private bool kabinSetPos = false;   // 花瓶がplayerの位置にいるかどうか

    protected override void Start()
    {
        base.Start();
        player = GameObject.FindGameObjectWithTag("Player");
        if (player == null)
            Debug.LogError("Not find Player");
    }

    private void KabinGimmickSetup()
    {
        Vector3 playerPosition = player.transform.position;
        playerPosition += player.transform.forward * 2.0f;

        KabinToPlayer(playerPosition);
    }

    private void KabinGimmickAction()
    {

        GimmickManager.Instance.ClearGimmick();
    }

    private void KabinToPlayer(Vector3 pPos)
    {
        while (transform.position != pPos)
        {
            transform.position = Vector3.MoveTowards(transform.position, pPos, speed);
        }

        kabinSetPos = true;
    }
    protected override void PushButtonGamePad(ControllerButton controller)
    {
        switch (controller)
        {
            // 方向選択に移行
            case ControllerButton.B:
                break;
            // 説明ボタン
            case ControllerButton.Y:
                break;

            case ControllerButton.A:
            case ControllerButton.X:
                NotButtonPushMessage();
                break;
            
        }
    }
}
