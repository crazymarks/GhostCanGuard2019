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
    [SerializeField]
    private float stunTime = 2.0f;
    
    //発動距離
    public float range = 1f;
    public RangeUI rangeui;

    private bool kabinSetPos = false;

    private bool IfActivated = false;
    private bool Broken = false;

    private Rigidbody rb = null;
    private MeshCollider meshCollider = null;

    private void Start()
    {
        _start();
        //GimmickEventSetUp(EventTriggerType.PointerDown, GimmickEventOpen);
        rb = GetComponent<Rigidbody>();
        Broken = false;
        IfActivated = false;
        if(player == null)
            player = GameObject.FindGameObjectWithTag("Player");
        rb.isKinematic = true;
        meshCollider = GetComponentInChildren<MeshCollider>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player"  || other.tag == "Ghost" ) return;

        if (IfActivated && other.tag == "Thief")
        {
            //泥棒のスタン処理
            var thief = other.GetComponent<Thief>();
            thief.GotStunned(stunTime);
        }
        if (IfActivated && other.tag == "Untagged")
        {
            rb.velocity = Vector3.zero;
            Debug.Log("Broken");
            Broken = true;
            Destroy(gameObject, stunTime-0.1f);
            //GimmickUIClose();
        }
    }
    private void Update()
    {
        if (st.stopped && !IfActivated)
        {
            if (!st.SecondPhase)
                rangeui.Show(range);
            else
            {
                rangeui.Hide();
            }
            if (GameManager.Instance.getXZDistance(gameObject, player) > range)
            {
                rangeui.SetColor(Color.red);
            }
            else
            {
                rangeui.SetColor(Color.green);
            }
        }
        else
        {
            rangeui.Hide();
        }
        if (gimmickUIParent.activeSelf)                              //UIが既に展開している場合
        {
            if (st.selectedObject != gameObject || st.SecondPhase)       //セレクトされていない　または　方向選択段階にいる場合
            {
                gimmickUIParent.SetActive(false);     //UIを収縮
            }
            if (GameManager.Instance.getXZDistance(gameObject, player) > range)
            {
                gimmickUIParent.GetComponent<DescriptionUIChange>().ActionUIHide();
            }
            else
            {
                gimmickUIParent.GetComponent<DescriptionUIChange>().ActionUIShow();
            }


        }
        else                                                                    // UIが展開していない場合
        {
            if (st.selectedObject == gameObject && !st.SecondPhase && !IfActivated)    //セレクトされたら、且つ、方向選択段階じゃない場合
            {
                gimmickUIParent.SetActive(true);                                   //UIを展開
                if (GameManager.Instance.getXZDistance(gameObject, player) > range)
                {
                    gimmickUIParent.GetComponent<DescriptionUIChange>().ActionUIHide();
                }
                else
                {
                    gimmickUIParent.GetComponent<DescriptionUIChange>().ActionUIShow();
                }
            }
        }

    }
    private void KabinGimmickSetup()
    {
        if (IfActivated) return;
        Vector3 playerPosition = player.transform.position;
        playerPosition += player.transform.forward;
        meshCollider.convex = false;
        KabinToPlayer(playerPosition);
       
    }

    private void KabinGimmickAction()
    {
        KabinGimmickSetup();
        if (IfActivated) return;
        if (!kabinSetPos) return;
        
        if (!st.SecondPhase)
        {
            st.gamestop(StopSystem.PauseState.DirectionSelect);
            player.GetComponent<Throw>().AimSlider.gameObject.SetActive(true);
            player.GetComponent<Throw>().MarkUI.SetActive(true);
            Debug.Log("Choose target");
            return;
        }

        meshCollider.convex = true;

        if (!Input.GetButtonDown("Send")) return;
        
        throwPos = Camera.main.ScreenToWorldPoint(st.getCursorScreenPosition());
        Debug.Log(throwPos);
        throwPos.y = 5.0f;

        // 力を加える方向をきめる
        Debug.Log("thorw!");
        
        Vector3 target = st.getCursorWorldPosition();
        player.transform.LookAt(new Vector3(target.x, gameObject.transform.position.y, target.z));
        rb.isKinematic = false;
        KabinToPlayer(player.transform.position + player.transform.forward);
        rb.AddForce(player.transform.forward * power + player.transform.up ,ForceMode.Impulse);
        IfActivated = true;
        player.GetComponent<Throw>().MarkUI.SetActive(false);

        GimmickManager.Instance.ClearGimmick();
        //GimmickUIClose();
        player.GetComponent<PlayerControl>().playerAnim.SetGimmickAnimation(GimmickAnimation.Revive);
        st.gamestop(StopSystem.PauseState.Normal);
        
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
                if (descriptionUIOn)
                {
                    HideDescription();
                }
                break;
            case ControllerButton.B:
                Debug.Log("Send");
                if (!descriptionUIOn)
                {
                    if (GameManager.Instance.getXZDistance(gameObject, player) <= range)
                        KabinGimmickAction();
                    //else
                    //{
                    //    StartCoroutine(GameManager.Instance.showTextWithSeconds("もっと近づいてください！", 1f));
                    //    st.gamestop();
                    //    GimmickUIClose();
                    //}
                }
                break;
            case ControllerButton.X:
                break;
            case ControllerButton.Y:
                if (!descriptionUIOn)
                {
                    ShowDescription("kabin");
                }
                break;
            case ControllerButton.Max:
                break;
            default:
                break;
        }
    }

}
