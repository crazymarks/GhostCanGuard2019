using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;

public class Horse : GimmickBase
{

    public enum HorseState
    {
        Sleep,
        Active,
        Back
    }

    HorseState horseState;

    [SerializeField]
    private bool IfActivated = false;   //使う中ですか
    private bool IfBacking = false;
    
    private Rigidbody HorseRB;
    private MeshRenderer HorseRenderer;
   
    //馬の速さ
    [SerializeField][Range(0,15)]
    private float HorseSpeed = 10.0f;
   
    [Range(0, 2)]
    public float HorseBackTime = 2.0f;
    [Range(0,1)]
    public float DispearAlpha =0.3f;
    float floorheight = 0;

    private float radius = 0.5f;
    [SerializeField]
    //private PlayerMove playerMove;
    private PlayerControl playerControl;
    [SerializeField]
    private GameObject Player;
    [SerializeField]
    private GameObject Saddle;                          //馬の鞍（くら）
    private Vector3 StartPosition = Vector3.zero;       //馬の初期位置
    private Quaternion startQuaternion = Quaternion.identity;

    Vector3 LeftOrient = Vector3.zero;
    Vector3 _moveorient = Vector3.zero;
    public Vector3 MoveOrient { get { return _moveorient; } set { _moveorient = value; } }


    protected override void Start()
    {
        base.Start();
        GimmickEventSetUp(EventTriggerType.PointerDown, GimmickEventOpen);
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
        try
        {
            playerControl = Player.GetComponent<PlayerControl>();
        }
        catch (NullReferenceException)
        {
            Debug.Log("プレイヤー未発見" + name);
        }
        
        
        if (Saddle == null)
        {
            Saddle = transform.Find("Saddle").gameObject;
        }
        
    }

    void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "Ghost" && IfActivated)
        {
            Debug.Log("殺人鬼をぶつけた!!");
            IfBacking = true;
            LeftOrient = Vector3.zero;
            GetOffHorse(Player, LeftOrient);
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
                if (Physics.Raycast(transform.position, _moveorient, out hit))
                {
                    //Debug.Log(hit.point);
                    //Debug.Log(hit.distance);
                    //Debug.Log(hit.collider);
                    //movedistance = Mathf.Clamp(movedistance, 0, hit.distance - radius > 0 ? hit.distance - radius : 0);
                    if (hit.distance <= radius && hit.collider.isTrigger == false)
                    {
                        Debug.Log("壁をぶつけた" + hit.distance + hit.collider + hit.point);
                        IfBacking = true;
                        LeftOrient = Vector3.zero;
                        GetOffHorse(Player, LeftOrient);
                    }
                    
                }
                
              
                if (Input.GetButton("Horizontal") || Input.GetButton("Vertical"))
                {
                    float horizontal = Input.GetAxis("Horizontal");
                    float vertical = Input.GetAxis("Vertical");
                    LeftOrient = new Vector3(horizontal, 0, vertical).normalized;
                    GetOffHorse(Player, LeftOrient);

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
            if (IfBacking)
            {
                Back();
            }
        }
        else return;
    }

    private void Back()
    {
        Stime += Time.fixedDeltaTime;
       
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
    }
 
    private void GetOnHorse(GameObject player)
    {
        tag = "Player";
       
        player.GetComponent<Rigidbody>().isKinematic = true;
        PlayerManager.Instance.SetCurrentState(PlayerState.Gimmick);
        player.transform.position = Saddle.transform.position;
        player.transform.rotation = Saddle.transform.rotation;
        player.transform.SetParent(this.transform);
        player.tag = "Untagged";
        Debug.Log("Saddle Set");
    } 
    private void GetOffHorse(GameObject player,Vector3 orient)
    {
        tag = "Gimmik";
        //player.transform.position += orient * transform.localScale.magnitude;
        //transform.parent = null;
        player.transform.parent = null;
        player.tag = "Player";
        //joint.connectedBody = null;
        player.transform.position = new Vector3(player.transform.position.x, floorheight, player.transform.position.z)+orient.normalized;
        PlayerManager.Instance.SetCurrentState(PlayerState.Play);
        LeftOrient = Vector3.zero;
        player.GetComponent<Rigidbody>().isKinematic = false;
        PlayerManager.Instance.SetCurrentState(PlayerState.Play);
        PlayerAnimationController.Instance.SetAnimatorValue(SetPAnimator.Push);
    }

    private void Active()
    {
        
        if (!IfActivated)
        {
            
            GetOnHorse(Player);
            IfActivated = true;
            
            
        }
        
        if (!Input.GetMouseButtonDown(0)) return;
        RaycastHit hitInfo;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hitInfo, 100))
        {
            Vector3 dir = new Vector3(hitInfo.point.x, transform.position.y, hitInfo.point.z);

            if (_moveorient == Vector3.zero)
            {
                MoveOrient = dir - transform.position;
                transform.rotation = Quaternion.LookRotation(MoveOrient);
            }
               
            
        }
        
       
        Debug.Log(_moveorient);
        GimmickUIClose();
    }

    public void OnClickUIStart()
    {
        if (GameManager.Instance.getXZDistance(gameObject, Player) <= 3)
            GimmickManager.Instance.SetGimmickAction(Active);
        else
        {
            StartCoroutine(GameManager.Instance.showTextWithSeconds("もっと近づいてください！", 1f));
            GimmickUIClose();
        }
        GimmickUIsOnOff(false);
    }
    
}
