using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hourse : MonoBehaviour
{
    [SerializeField]
    private bool IfEnable = false;
    protected bool IfUsed = false;
    //馬の速さ
    [SerializeField]
    private float HourseSpeed = 10.0f;
    //馬の移動時間
    [SerializeField]
    public float HourseMoveTime = 3.0f;

    float floorheight = 0;

    private Rigidbody HourseRB;

    public static Hourse instanceHourse;

    private bool _HourseMove = false;
    public bool IsHourseMove { get { return _HourseMove; } set { _HourseMove = value; } }

    private float radius = 1f;
    [SerializeField]
    private PlayerMove playerMove;
    [SerializeField]
    private GameObject Player;
    [SerializeField]
    private GameObject Saddle;                          //馬の鞍（くら）
    // Start is called before the first frame update
    void OnEnable()
    {
        IfEnable = true;
        HourseRB = this.gameObject.GetComponent<Rigidbody>();
    }


    // Update is called once per frame
    void Update()
    {
        if (!IsHourseMove) return;
        Move(HourseSpeed, HourseMoveTime);
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!IfUsed && !IsHourseMove && IfEnable &&other.tag=="Player")
        {
            IfEnable = false;
            GetOnHourse(Player);
            IsHourseMove = true;
            //StartCoroutine(HourseMove(other.gameObject, HourseSpeed, GetOrient()));
            
        }
        
    }
    private void OnTriggerExit(Collider other)
    {
        IfEnable = false;
        Stime = 0;
        Debug.Log("Reade to Reuse");
    }

   
          
    private Vector3 GetOrient()                                                  
    {// 右
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            return Vector3.right.normalized;
        }
        // 左
        else if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            return Vector3.left.normalized;
        }
        // 上
        else if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
        {
            return Vector3.forward.normalized;
        }
        // 下
        else if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
        {
            return Vector3.back.normalized;
        }
        else return MoveOrient.normalized;
        
    }
    Vector3 MoveOrient = Vector3.zero;
    float Stime=0;
    private void Move(float speed, float time)
    {
        
        //StartCoroutine(HourseMoveTImeCountDown(time));
        MoveOrient = GetOrient() * speed;
        Debug.Log(MoveOrient);
        transform.position += MoveOrient * Time.deltaTime;
        Stime += Time.deltaTime;
        if (Stime > time)
        {
            GetOffHourse(Player, MoveOrient.normalized);
            IsHourseMove = false;
            return;
        }
       
    }

    IEnumerator HourseMoveTImeCountDown(float time)
    {
        //Debug.Log(orient);
        //transform.position +=orient * speed/60;
        //IfEnable = false;
        //if ()
        //{

        //   yield break;
        //}
        yield return new WaitForSeconds(time);
        GetOffHourse(Player,MoveOrient.normalized);
        IsHourseMove = false;
    }
    private void GetOnHourse(GameObject player)
    {
        playerMove.IsPlayerMove = false;
        player.transform.position = Saddle.transform.position;
        player.transform.SetParent(this.transform);
        Debug.Log("Saddle Set");
        IfUsed = true;
    } 
    private void GetOffHourse(GameObject player,Vector3 orient)
    {
       
        player.transform.position += orient * this.transform.localScale.magnitude;
        player.transform.parent = null;
        player.transform.position = new Vector3(player.transform.position.x, floorheight, player.transform.position.z);
        playerMove.IsPlayerMove = true;
    }
    public void OnOff()      
    {
        IfEnable = !IfEnable;
    }


}
