using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Throw : GimmickBase
{
    public Slider AimSlider;
    public GameObject HolyWater;
    public float force=10;
    [Range(0, 10)]
    public int count = 1;       //残り弾数
    private bool IfActivated;
    
    // Start is called before the first frame update
    private void Start()
    {
        _start();
        //st = GameManager.Instance.GetComponent<stop>();
    }

    // Update is called once per frame
    void Update()
    {
        //if (Input.GetButtonDown("Send") && st.selectedObject == gameObject && (!IfActivated || st.SecondPhase == true))
        //{
        //    throwHolyWater();
        //}
        if (gimmickUIParent.activeSelf)                              //UIが既に展開している場合
        {
            if (st.selectedObject != gameObject || st.SecondPhase)       //セレクトされていない　または　方向選択段階にいる場合
            {
                gimmickUIParent.SetActive(false);     //UIを収縮
            }
        }
        else                                                                    // UIが展開していない場合
        {
            if (st.selectedObject == gameObject && !st.SecondPhase)    //セレクトされたら、且つ、方向選択段階じゃない場合
                gimmickUIParent.SetActive(true);                                   //UIを展開
        }
    }

    void throwHolyWater(){
        if (count <= 0) {
            Debug.Log("弾が切れた");
            return;
        }
        if(!IfActivated)
            IfActivated = true;
        if (!st.SecondPhase)
        {
            st.changeToSecondPhase();
            AimSlider.gameObject.SetActive(true);
            Debug.Log("Choose target");
            return;
        }
        if (!Input.GetButtonDown("Send")) return;
        PlayerrAnimationController.Instance.SetGimmickAnimation(GimmickAnimation.Float);
        Vector3 target = st.getCursorWorldPosition();
        transform.LookAt(new Vector3(target.x, gameObject.transform.position.y, target.z));
        GameObject holywater = Instantiate(HolyWater, new Vector3(gameObject.transform.position.x, 1, gameObject.transform.position.z) + gameObject.transform.forward, Quaternion.identity);
        Debug.Log(target);
        Rigidbody rb = holywater.GetComponent<Rigidbody>();
        rb.AddForce(transform.forward * force, ForceMode.Impulse);
        st.gamestop();
        IfActivated = false;
        count--;
    }

    protected override void PushButtonGamePad(ControllerButton controller)
    {
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
                    throwHolyWater();
                }
               
                break;
            case ControllerButton.X:
                break;
            case ControllerButton.Y:
                if (!descriptionUIOn)
                {
                    ShowDescription("holywater");
                }
                break;
            case ControllerButton.Max:
                break;
            default:
                break;
        }
    }


}
