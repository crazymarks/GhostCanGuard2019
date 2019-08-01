using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class KabinGimmick : GimmickBase
{
    private Vector3 throwPos = Vector3.zero;   // 花瓶が飛んでいく場所

    [SerializeField]
    private float power = 5.0f; // 花瓶を押す力
    [SerializeField]
    private GameObject player = null;
    [SerializeField]
    private float speed = 2.0f;

    private bool kabinSetPos = false;

    private bool IfActivated = false;
    private bool Broken = false;

    private Rigidbody rb = null;

    protected override void Start()
    {
        base.Start();
        GimmickEventSetUp(EventTriggerType.PointerDown, GimmickEventOpen);
        rb = GetComponent<Rigidbody>();
        Broken = false;
        IfActivated = false;
        player = GameObject.FindGameObjectWithTag("Player");
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player"  || other.tag == "Ghost" ) return;

        if (IfActivated && other.tag == "Thief")
        {
            //泥棒の処理
        }
        if (IfActivated && other.tag == "Untagged")
        {
            rb.velocity = Vector3.zero;
            Debug.Log("Broken");
            Broken = true;
            Destroy(gameObject, 2f);
            GimmickUIClose();
        }
    }

    private void KabinGimmickSetup()
    {
        if (IfActivated) return;
        Vector3 playerPosition = player.transform.position;
        playerPosition += player.transform.forward;

        KabinToPlayer(playerPosition);
       
    }

    private void KabinGimmickAction()
    {
        KabinGimmickSetup();
        if (IfActivated) return;
        if (!kabinSetPos) return;
        
        if (!st.SecondPhase)
        {
            st.changeToSecondPhase();
            Debug.Log("Choose target");
            return;
        }
        

        if (!Input.GetButtonDown("Send")) return;
        
        throwPos = Camera.main.ScreenToWorldPoint(st.getCursorScreenPosition());
        Debug.Log(throwPos);
        throwPos.y = 5.0f;

        // 力を加える方向をきめる
        Debug.Log("thorw!");
        
        Vector3 target = st.getCursorWorldPosition();
        player.transform.LookAt(new Vector3(target.x, gameObject.transform.position.y, target.z));

        KabinToPlayer(player.transform.position + player.transform.forward);
        rb.AddForce(player.transform.forward * power + player.transform.up ,ForceMode.Impulse);
        IfActivated = true;

        GimmickManager.Instance.ClearGimmick();
        GimmickUIClose();
        st.gamestop();
        
    }

    //// ButtonのonClickで呼ぶ関数
    //public void ClickUIStart()
    //{
    //    GimmickManager.Instance.SetGimmickAction(KabinGimmickAction);
    //    GimmickUIsOnOff(false);
    //}

    private void KabinToPlayer(Vector3 pPos)
    {
        while(transform.position != pPos)
        {
            transform.position = Vector3.MoveTowards(transform.position, pPos, speed);
            
        }

        kabinSetPos = true;
    }
    protected override void PushButtonGamePad(ControllerButton controller)
    {
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
        }
    }
}
