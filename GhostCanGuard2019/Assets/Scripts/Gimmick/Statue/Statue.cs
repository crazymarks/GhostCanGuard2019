using UnityEngine;

public class Statue : GimmickBase
{
    //public float thrust;
    public GameObject SmokeAura = null; //彫像のエフェクト
    //public Rigidbody rb;
    [SerializeField]
    GameObject AirWall = null;
    [SerializeField]
    GameObject KinghtModel;

    public float smooth = 3f;
    float tiltAngle = 0f;

    bool isFallen = false;

    public GameObject tiltpoint;
    float tiltAroundX = 0f;
    float tiltAroundZ = 0f;

    private void Start()
    {
        _start();
        //rb = GetComponent<Rigidbody>();
        //GimmickEventSetUp(EventTriggerType.PointerDown, GimmickEventOpen);
        //rb.isKinematic = true;
        tiltAroundX = 0f;
        tiltAroundZ = 0f;
        tiltAngle = 0f;
        transform.localPosition = new Vector3(transform.localPosition.x, 0f, transform.localPosition.z);
        AirWall.SetActive(false);

    }

    private void FixedUpdate()
    {
        Quaternion target = Quaternion.Euler(tiltAroundX, transform.rotation.eulerAngles.y, tiltAroundZ);

        //transform.rotation = Quaternion.Slerp(transform.rotation, target, Time.deltaTime * smooth);
        if (Mathf.Abs(KinghtModel.transform.rotation.eulerAngles.x) < tiltAngle)
        {
            KinghtModel.transform.RotateAround(tiltpoint.transform.position, transform.right, smooth);
        }
        
        if (isFallen)
        {
            KinghtModel.transform.position = new Vector3(transform.localPosition.x, 0.5f, transform.localPosition.z);
        }
        
    }
    private void Update()
    {
        //if (Input.GetButtonDown("Send") && st.selectedObject == gameObject)
        //{
        //    FallForwards();
        //}
        if (isFallen) return;
        if (gimmickUIParent.activeSelf)                              //UIが既に展開している場合
        {
            if (st.selectedObject != gameObject || st.SecondPhase)       //セレクトされていない　または　方向選択段階にいる場合
            {
                gimmickUIParent.SetActive(false);     //UIを収縮
            }
        }
        else                                                                    // UIが展開していない場合
        {
            if (st.selectedObject == gameObject && !st.SecondPhase && !isFallen)    //セレクトされたら、且つ、方向選択段階じゃない場合
                gimmickUIParent.SetActive(true);                                   //UIを展開
        }
    }
    // for buttons
    //public void FallRight()
    //{
    //    tiltAroundZ = tiltAngle;
    //    //rb.AddForce(transform.right * thrust);
    //    GimmickUIClose();
    //}

    //public void FallLeft()
    //{
    //    tiltAroundZ = -tiltAngle;
    //    //rb.AddForce(-transform.right * thrust);
    //    GimmickUIClose();
    //}

    public void FallForwards()
    {
        if (!isFallen)
        {
            tiltAngle = 90f;
            isFallen = true;
            gimmickUIParent.SetActive(false);
            st.gamestop(StopSystem.PauseState.Normal);
            AudioController.PlaySnd("B6_StatueDown", Camera.main.transform.position, 1f);
            AirWall.SetActive(true);
            SmokeAura.SetActive(true);  　　　　　　　　　　　　　　　　//エフェクト起動
        }
       
        //rb.AddForce(transform.forward * thrust);

    }

    //public void FallBackwards()
    //{
    //    tiltAroundX = -tiltAngle;
    //    //rb.AddForce(-transform.forward * thrust);
    //    GimmickUIClose();
    //}

    public void resetPillar()
    {
        tiltAroundX = 0;
        tiltAroundZ = 0;
        isFallen = false;
    }


    //public void CloseUI()
    //{
    //    GimmickUIClose();
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
                Debug.Log("Fall");
                if (!descriptionUIOn)
                {
                    FallForwards();
                }
               
                break;
            case ControllerButton.X:
                break;
            case ControllerButton.Y:
                if (!descriptionUIOn)
                {
                    ShowDescription("statue");
                }
                break;
            case ControllerButton.Max:
                break;
            default:
                break;
        }
    }
}
