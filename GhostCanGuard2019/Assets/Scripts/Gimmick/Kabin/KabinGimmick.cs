using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class KabinGimmick : GimmickBase
{
    private Vector3 throwPos = Vector3.zero;   // 花瓶が飛んでいく場所

    [SerializeField] private float power = 5.0f; // 花瓶を押す力
    private GameObject player = null;

<<<<<<< HEAD
    [SerializeField] private float speed = 2.0f;    // speed when kabin move to player
    private bool kabinSetPos = false;   // 花瓶がplayerの位置にいるかどうか
=======
    private bool kabinSetPos = false;

    private bool IfActivated = false;
    private bool Broken = false;

    private Rigidbody rb = null;
>>>>>>> origin/wangguanyu

    protected override void Start()
    {
        base.Start();
<<<<<<< HEAD
        player = GameObject.FindGameObjectWithTag("Player");
        if (player == null)
            Debug.LogError("Not find Player");
=======
        GimmickEventSetUp(EventTriggerType.PointerDown, GimmickEventOpen);
        rb = GetComponent<Rigidbody>();
        Broken = false;
        IfActivated = false;
        player = GameObject.FindGameObjectWithTag("Player");
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player"  || other.tag == "Ghost") return;
        
        if (IfActivated)
        {
            if (other.tag == "Thief")
            {

            }
            rb.velocity = Vector3.zero;
            Debug.Log("Broken");
            Broken = true;
            Destroy(gameObject, 2f);
        }
>>>>>>> origin/wangguanyu
    }

    private void KabinGimmickSetup()
    {
        Vector3 playerPosition = player.transform.position;
        playerPosition += player.transform.forward * 2.0f;

        KabinToPlayer(playerPosition);
    }

    private void KabinGimmickAction()
    {
<<<<<<< HEAD

        GimmickManager.Instance.ClearGimmick();
    }
=======
        KabinGimmickSetup();
        
        if (!kabinSetPos) return;
        
        if (!st.SecondPhase)
        {
            st.SecondPhase = true;
            Debug.Log("Choose target");
            return;
        }
        

        if (!Input.GetButtonDown("Send")) return;
        
        throwPos = Camera.main.ScreenToWorldPoint(st.getCursorWorldPosition());
        Debug.Log(throwPos);
        throwPos.y = 5.0f;

        // 力を加える方向をきめる
        Debug.Log("thorw!");
        
        Vector3 target = st.getCursorWorldPosition();
        player.transform.LookAt(new Vector3(target.x, gameObject.transform.position.y, target.z));

        KabinToPlayer(player.transform.position+player.transform.forward);
        rb.AddForce(player.transform.forward * power + player.transform.up, ForceMode.Impulse);
        IfActivated = true;

        GimmickManager.Instance.ClearGimmick();
        GimmickUIClose();
        st.SecondPhase = false;
        st.gamestop();
        
    }

    //// ButtonのonClickで呼ぶ関数
    //public void ClickUIStart()
    //{
    //    GimmickManager.Instance.SetGimmickAction(KabinGimmickAction);
    //    GimmickUIsOnOff(false);
    //}
>>>>>>> origin/wangguanyu

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
<<<<<<< HEAD
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
            
=======
        base.PushButtonGamePad(controller);
        switch (controller)
        {
            case ControllerButton.A:
                break;
            case ControllerButton.B:
                Debug.Log("Send");
                KabinGimmickAction();
                break;
            case ControllerButton.X:
                break;
            case ControllerButton.Y:
                break;
            case ControllerButton.Max:
                break;
            default:
                break;
>>>>>>> origin/wangguanyu
        }
    }
}
