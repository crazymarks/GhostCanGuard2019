using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Bible : GimmickBase
{
   
    public float buffTime = 5f;
    public float coolDownTime = 5f;
    public float auraTime = 5f;
    public float auraRadius = 5f;

    bool isOpen;
    bool canOpen;
    float startTime;
    [SerializeField]
    SphereCollider collision = null;

    ParticleSystem aura;

    GameObject obj;
    //Ghost_targeting gt;
    // Start is called before the first frame update
    private void Start()
    {
        _start();
        //GimmickEventSetUp(EventTriggerType.PointerDown, GimmickEventOpen);
       
        if (collision != null)
        {
            collision.enabled = false;
            collision.radius = auraRadius;
        }
        if (aura == null)
        {
            aura = GetComponentInChildren<ParticleSystem>();
        }
        aura.Stop();
        aura.Clear();
        isOpen = false;
        canOpen = true;
    }

   

    private void Update()
    {
        if (isOpen) return;
        if (gimmickUIParent.activeSelf)                                  //UIが既に展開している場合
        {
            if (st.selectedObject != gameObject || st.SecondPhase)       //セレクトされていない　または　方向選択段階にいる場合
            {
                gimmickUIParent.SetActive(false);     //UIを収縮
            }
        }
        else                                                                    // UIが展開していない場合
        {
            if (st.selectedObject == gameObject && !st.SecondPhase && canOpen)            //セレクトされたら、且つ、方向選択段階じゃない場合
                gimmickUIParent.SetActive(true);                                   //UIを展開
        }
    }


    private void open()
    {
        if (canOpen)
        {
            StartCoroutine(AuraON());
            gimmickUIParent.SetActive(false);     //UIを収縮
            st.gamestop(StopSystem.PauseState.Normal);
            AudioController.PlaySnd("A6_SuperPower", Camera.main.transform.position, 1f);
        }
        else
        {
            if (isOpen)
                Debug.Log("起動中です");
            else
                Debug.Log("準備中です");
        }
        
        //GimmickUIClose();

    }
    IEnumerator AuraON()
    {
        isOpen = true;
        collision.enabled = true;
        collision.GetComponent<BibleAura>().buffTime = buffTime;
        aura.Play();
        canOpen = false;
        yield return new WaitForSeconds(auraTime);
        isOpen = false;
        aura.Stop();
        collision.enabled = false;
        yield return new WaitForSeconds(coolDownTime);
        canOpen = true;
        Debug.Log("Ready to ReUse");
    }

    //public void ClickUIStart()
    //{
    //    GimmickManager.Instance.SetGimmickAction(open);
    //    GimmickUIsOnOff(false);
    //}
    //private void Update()
    //{
    //    if (Input.GetButtonDown("Send") && st.selectedObject == gameObject)
    //    {
    //        open();
    //    }
    //}


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
                    open();
                }

                break;
            case ControllerButton.X:
                break;
            case ControllerButton.Y:
                if (!descriptionUIOn)
                {
                    ShowDescription("bible");
                }
                break;
            case ControllerButton.Max:
                break;
            default:
                break;

        }
    }
}
