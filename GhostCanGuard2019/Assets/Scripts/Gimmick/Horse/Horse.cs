using UnityEngine;
using UnityEngine.UI;

public class Horse : GimmickBase
{

    public enum HorseState
    {
        Sleep,
        Active,
        Back
    }

    HorseState horseState;

    public GameObject MarkUI;
    public Slider AimSlider;

    [SerializeField]
    private bool IfActivated = false;   //使う中ですか
    private bool IfBacking = false;
    private bool IfMoving = false;

    private Rigidbody HorseRB;
    private MeshRenderer HorseRenderer;
   
    //馬の速さ
    [SerializeField][Range(0,21)]
    private float HorseSpeed = 10.0f;
    //発動距離
    public float range = 5f;
    public RangeUI rangeui;
    public string OutRangeMsg;
    public string InRangeMsg;


    [Range(0, 2)]
    public float HorseBackTime = 2.0f;
    [Range(0,1)]
    public float DispearAlpha =0.3f;
    float floorheight = 0;



    private float radius = 0.5f;

    //private PlayerControl playerControl;
    [SerializeField]
    private GameObject Player;
    [SerializeField]
    private GameObject Saddle;                          //馬の鞍（くら）
    private Vector3 StartPosition = Vector3.zero;       //馬の初期位置
    public GameObject RidePlayer;
    public PlayerAnimationController RideAnimation;

    private Quaternion startQuaternion = Quaternion.identity;

    Vector3 LeftOrient = Vector3.zero;
    Vector3 _moveorient = Vector3.zero;
    public Vector3 MoveOrient { get { return _moveorient; } set { _moveorient = value; } }
    
    /// <summary>
    /// ParticalSystem
    /// </summary>
    public GameObject Cloud;
    public GameObject Fly;

    /// <summary>
    /// FadeOut
    /// </summary>
    GetHorseMaterial horseMat;


    private void Start()
    {
        _start();
        //GimmickEventSetUp(EventTriggerType.PointerDown, GimmickEventOpen);
        tag = "Gimmik";
        StartPosition = transform.position;
        startQuaternion = transform.localRotation;
        
        HorseRB = GetComponent<Rigidbody>();
        HorseRenderer = GetComponent<MeshRenderer>();
        HorseRB.isKinematic = true;
        if (Player == null)
        {
            Player = GameObject.FindGameObjectWithTag("Player");
        }


        horseMat = GetComponentInChildren<GetHorseMaterial>();
        MarkUI.SetActive(false);

        if (Saddle == null)
        {
            Saddle = transform.Find("Saddle").gameObject;
        }
        resetHorse();
        RidePlayer.SetActive(false);
    }

    void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "Ghost" && IfMoving)
        {
            collision.gameObject.GetComponent<Ghost_targeting>().HolyWater(5f);
            Debug.Log("殺人鬼をぶつけた!!");
            IfBacking = true;
            LeftOrient = Vector3.zero;
            GetOffHorse(Player, LeftOrient);
        }
    }

    private void Update()
    {
        if(st.stopped && !IfActivated)
        {
            if(!st.SecondPhase)
                rangeui.Show(range);
            else
            {
                rangeui.Hide();
            }
            if (GameManager.Instance.getXZDistance(gameObject, Player) > range)
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
            if (GameManager.Instance.getXZDistance(gameObject, Player) > range)
            {
                gimmickUIParent.GetComponent<DescriptionUIChange>().ActionUIShow(OutRangeMsg);
            }
            else
            {
                gimmickUIParent.GetComponent<DescriptionUIChange>().ActionUIShow(InRangeMsg);
            }


        }
        else                                                                    // UIが展開していない場合
        {
            if (st.selectedObject == gameObject && !st.SecondPhase && !IfActivated)    //セレクトされたら、且つ、方向選択段階じゃない場合
            {
                gimmickUIParent.SetActive(true);                                   //UIを展開
                if (GameManager.Instance.getXZDistance(gameObject, Player) > range)
                {
                    gimmickUIParent.GetComponent<DescriptionUIChange>().ActionUIShow(OutRangeMsg);
                }
                else
                {
                    gimmickUIParent.GetComponent<DescriptionUIChange>().ActionUIShow(InRangeMsg);
                }
            }
        }
        if (MarkUI.activeSelf)
        {
            MarkUI.GetComponentInChildren<MarkUI>().setUIPosition();
        }
        if (IfActivated && !IfBacking)
        {
            if (IfMoving && Input.GetButtonDown("Cancel") || GameManager.Instance.gameover)
            {
                GetOffHorse(Player, Vector3.zero);
                if (transform.position == StartPosition)
                {
                    resetHorse();
                }
                else
                {
                    IfBacking = true;
                    Debug.Log("Start Back");
                }
            }
        }
    }

    void FixedUpdate()
    {
        MoveUpdate();
    }
   
 
    float Stime = 0;
    private void Move(float speed,Vector3 orient)
    {
        if (orient == Vector3.zero) return;
        float movedistance = (speed / Mathf.Sqrt(2.0f) * Time.deltaTime);
        Vector3 Direction = orient.normalized * movedistance;
        //Debug.Log(Direction);
        
        //transform.rotation = Quaternion.LookRotation(orient);
        transform.position += transform.forward.normalized * movedistance;
    }
    private void MoveUpdate()
    {
        if (IfActivated)
        {
            Move(HorseSpeed,_moveorient);
           
            if (!IfBacking) 
            {
                RaycastHit hit;
                if (Physics.Raycast(transform.position,transform.forward, out hit))
                {
                    //Debug.DrawRay(transform.position, transform.forward,Color.red);
                    //movedistance = Mathf.Clamp(movedistance, 0, hit.distance - radius > 0 ? hit.distance - radius : 0);
                    if (hit.distance <= radius && hit.collider.isTrigger == false)
                    {
                        Debug.Log("壁をぶつけた" + hit.distance + hit.collider + hit.point);
                        IfBacking = true;
                        LeftOrient = Vector3.zero;
                        GetOffHorse(Player, LeftOrient);
                    }
                    
                }
            }
            else
            {
                Back();
            }
        }
        else return;
    }

    private void Back()
    {
        Stime += Time.fixedDeltaTime;
        horseMat.alpha = 1 - Stime*2 / HorseBackTime;
        horseMat.UpdateAlpha();
        if (Stime> HorseBackTime)
        {
            resetHorse();
            Stime = 0;
        }
    }
   private void resetHorse()
    {
        transform.position = StartPosition;
        transform.rotation = startQuaternion;
        IfBacking = false;
        Debug.Log("Backing End");
        IfActivated = false;
        GimmickManager.Instance.ClearGimmick();
        MoveOrient = Vector3.zero;
        Debug.Log("Ready to Reuse");
        Cloud.SetActive(false);
        Fly.SetActive(false);
        horseMat.alpha = 1;
        horseMat.UpdateAlpha();
    }
 
    private void GetOnHorse(GameObject player)
    {
        tag = "Player";
        player.GetComponent<Rigidbody>().isKinematic = true;
        PlayerManager.Instance.SetCurrentState(PlayerState.Gimmick);
        player.transform.position = transform.position;
        player.transform.rotation = transform.rotation;
        player.transform.SetParent(this.transform);
        player.SetActive(false);

        //Animation
        RidePlayer.SetActive(true);
        RidePlayer.transform.localPosition = Saddle.transform.localPosition;
        RidePlayer.transform.localRotation = Saddle.transform.localRotation;
        RideAnimation.SetAnimaionByName("Kebiyin_HorseRide");
        
        player.tag = "Untagged";
        Debug.Log("Saddle Set");
        Cloud.SetActive(true);
    } 
    private void GetOffHorse(GameObject player,Vector3 orient)
    {

        //Animation
        RideAnimation.SetGimmickAnimation(GimmickAnimation.None);
        RidePlayer.SetActive(false);
        
        tag = "Gimmik";
        //player.transform.position += orient * transform.localScale.magnitude;
        //transform.parent = null;
        player.transform.parent = null;
        player.tag = "Player";
        player.SetActive(true);
        //joint.connectedBody = null;
        player.transform.position = new Vector3(player.transform.position.x, floorheight, player.transform.position.z) + orient.normalized;
        PlayerManager.Instance.SetCurrentState(PlayerState.Play);
        LeftOrient = Vector3.zero;
        player.GetComponent<Rigidbody>().isKinematic = false;
        PlayerManager.Instance.SetCurrentState(PlayerState.Play);

        MarkUI.GetComponent<DescriptionUIChange>().DescriptionOnOff();
        MarkUI.SetActive(false);
        //PlayerAnimationController.Instance.SetAnimatorValue(SetPAnimator.Push);
        IfMoving = false;
    }

    private void Active()
    {
        
        if (!IfActivated)
        {
            
            GetOnHorse(Player);
            IfActivated = true;
            if (!st.SecondPhase)
            {
                st.gamestop(StopSystem.PauseState.DirectionSelect);
                MarkUI.SetActive(true);
                AimSlider.gameObject.SetActive(true);
                Debug.Log("Choose target");
                return;
            }
        }
       
        
        if (!Input.GetButtonDown("Send")) return;
        RaycastHit hitInfo;
        //Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Ray ray = Camera.main.ScreenPointToRay(st.cursor.transform.position);
        if (Physics.Raycast(ray, out hitInfo, 100))
        {
            Vector3 dir = new Vector3(hitInfo.point.x, transform.position.y, hitInfo.point.z);

            if (_moveorient == Vector3.zero)
            {
                MoveOrient = dir - transform.position;
                transform.rotation = Quaternion.LookRotation(MoveOrient);
            }


        }
       
        MarkUI.GetComponent<DescriptionUIChange>().DescriptionOnOff();
        IfMoving = true;
        Fly.SetActive(true);
        Debug.Log(_moveorient);

        //Animation
        RidePlayer.transform.localPosition = new Vector3(0, -0.7f, -0.75f) ;
        RidePlayer.transform.localRotation = Quaternion.identity;
        RideAnimation.SetGimmickAnimation(GimmickAnimation.HorseRun);


        st.gamestop(StopSystem.PauseState.Normal);
        //GimmickUIClose();
        AudioController.PlaySnd("A8_HorseScream",Camera.main.transform.position, 1f);
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
                if (!descriptionUIOn && !IfMoving)
                {
                    if (GameManager.Instance.getXZDistance(gameObject, Player) <= range)
                        Active();
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
                    ShowDescription("horse");
                }
                break;
            case ControllerButton.Max:
                break;
            default:
                break;
        }
    }
}
