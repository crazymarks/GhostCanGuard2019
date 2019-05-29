using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hourse : GimmickBase
{
    [SerializeField]
    private bool IfEnable = false;      //使いできますか(UIエフェクト、光、説明文、移動メニュー)
    private bool IfActivated = false;   //使う中ですか
    private bool IfBacking = false;
    private Rigidbody HourseRB;
    private MeshRenderer HourseRenderer;
    public static Hourse instanceHourse;
   
    //馬の速さ
    [SerializeField][Range(0,15)]
    private float HourseSpeed = 10.0f;
    //馬の移動時間
    //[Range(0, 15)]
    //public float HourseMoveTime = 3.0f;
    [Range(0, 2)]
    public float HourseBackTime = 2.0f;
    [Range(0,1)]
    public float DispearAlpha =0.3f;
    float floorheight = -1;

    private float radius = 0.5f;
    [SerializeField]
    private PlayerMove playerMove;
    [SerializeField]
    private GameObject Player;
    [SerializeField]
    private GameObject Saddle;                          //馬の鞍（くら）
    private Vector3 StartPosition = Vector3.zero;       //馬の初期位置

    void Awake()
    {
        tag = "Gimmik";
        StartPosition = transform.position;
        IfEnable = true;
        HourseRB = GetComponent<Rigidbody>();
        HourseRenderer = GetComponent<MeshRenderer>();
    }


    // Update is called once per frame
    void FixedUpdate()
    {
        MoveUpdate();
    //    if (!IsHourseMove) return;
    // Move(HourseSpeed, HourseMoveTime);

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Active(Vector3.forward.normalized);
        }
        else return;

    }

    public void Active(Vector3 orient)
    {
        if (!IfActivated && IfEnable )
        {
            IfEnable = false;
            MoveOrient = orient;
            GetOnHourse(Player);
            // StartCoroutine(HourseMove(HourseMoveTime));
            IfActivated = true;
            //IsHourseMove = true;
            //StartCoroutine(HourseMove(other.gameObject, HourseSpeed, GetOrient()));

        }
    }
    private void OnTriggerExit(Collider other)
    {
        
        
    }
    

    
    Vector3 LeftOrient = Vector3.zero;
    Vector3 _moveorient = Vector3.zero;
    public  Vector3 MoveOrient { get { return _moveorient; } set { _moveorient = value; } }
   
    private bool GetKeyInput(Vector3 input)
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow)) return true;
        else return false;
    }
    float Stime = 0;
    private void Move(float speed,Vector3 orient)
    {
        float movedistance = (speed / Mathf.Sqrt(2.0f) * Time.deltaTime);
        RaycastHit hit;
        if (!IfBacking)
        {
            if (Physics.Raycast(transform.position, orient, out hit, movedistance))
            {
                Debug.Log(hit.point);
                Debug.Log(hit.distance);
                Debug.Log(hit.collider);
                movedistance = Mathf.Clamp(movedistance, 0, hit.distance - radius > 0 ? hit.distance - radius : 0);
                if (movedistance <= 0)
                {
                    Debug.Log("壁をぶつけた");
                    IfBacking = true;
                    LeftOrient = -hit.normal;
                    GetOffHourse(Player, LeftOrient);
                }
            }

        }
       
        Vector3 Direction = transform.position + orient.normalized * movedistance;
        Debug.Log(orient);
        transform.position = Vector3.MoveTowards(transform.position, Direction,movedistance);
    }
    private void MoveUpdate()
    {
        if (IfActivated)
        {
            Move(HourseSpeed,_moveorient);
           
            if (!IfBacking) 
            {
                if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
                {
                    IfBacking = true;
                    Debug.Log("Start Back");
                    //LeftOrient = Vector3.right;
                    //MoveOrient = Vector3.zero;
                    GetOffHourse(Player, LeftOrient);
                }
                else if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
                {
                    IfBacking = true;
                    Debug.Log("Start Back");
                    //LeftOrient = Vector3.left;
                    //MoveOrient = Vector3.zero;
                    GetOffHourse(Player, LeftOrient);
                }
                else if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
                {
                    IfBacking = true;
                    Debug.Log("Start Back");
                    //LeftOrient = Vector3.up;
                    //MoveOrient = Vector3.zero;
                    GetOffHourse(Player, LeftOrient);
                }
                else if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
                {
                    IfBacking = true;
                    Debug.Log("Start Back");
                    //LeftOrient = Vector3.down;
                    //MoveOrient = Vector3.zero;
                    GetOffHourse(Player, LeftOrient);
                }
                //else if (Stime > HourseMoveTime )
                //{
                //    Debug.Log("Start Back");
                //    IfBacking = true;
                //    LeftOrient = MoveOrient;
                //    //MoveOrient = Vector3.zero;
                //    GetOffHourse(Player, LeftOrient);
                //}
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
        // transform.position = Vector3.MoveTowards(transform.position, StartPosition, HourseSpeed * Time.deltaTime);       //元の位置に戻る
        Stime += Time.fixedDeltaTime;
        //HourseRenderer.material.color = new Color(HourseRenderer.material.color.r, HourseRenderer.material.color.g, HourseRenderer.material.color.b,1-Mathf.Clamp(Stime/HourseBackTime, 0, 1-DispearAlpha));
        //if (transform.position == StartPosition)
        if(Stime> HourseBackTime)
        {
            transform.position = StartPosition;
            IfBacking = false;
            //HourseRenderer.material.color = new Color(HourseRenderer.material.color.r, HourseRenderer.material.color.g, HourseRenderer.material.color.b,1);

            Debug.Log("Backing End");
           
            IfActivated = false;

            IfEnable = true;
            Debug.Log("Reade to Reuse");
            Stime = 0;
        }
    }
    //IEnumerator HourseMove(float time)                    //IEnumerator を使う時間制御　　まだ未完成
    //{
    //    Stime += Time.fixedDeltaTime;
    //    HourseRB = GetComponent<Rigidbody>();
    //    HourseRB.velocity = MoveOrient * HourseSpeed;
    //    yield return new WaitForSeconds(time);
    //    HourseRB.velocity = Vector3.zero;

    //}
    private void GetOnHourse(GameObject player)
    {
        tag = "Player";
        playerMove.IsPlayerMove = false;
        player.transform.position = Saddle.transform.position;
        player.transform.SetParent(this.transform);
        player.tag = "Untagged";
        Debug.Log("Saddle Set");
    } 
    private void GetOffHourse(GameObject player,Vector3 orient)
    {
        tag = "Gimmik";
        //player.transform.position += orient * transform.localScale.magnitude;
        player.transform.parent = null;
        player.tag = "Player";
        player.transform.position = new Vector3(player.transform.position.x, floorheight, player.transform.position.z);
        playerMove.IsPlayerMove = true;
        LeftOrient = Vector3.zero;
    }
    public void OnOff()      
    {
        IfEnable = !IfEnable;
    }


}
