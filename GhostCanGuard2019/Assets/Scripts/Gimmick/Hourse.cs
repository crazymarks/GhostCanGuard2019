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
    public float starttime = 0f;
    private bool moving=false;

    private Rigidbody HourseRB;

    public static Hourse instanceHourse;

    private bool _HourseMove = true;
    public bool IsHourseMove { get { return _HourseMove; } set { _HourseMove = value; } }

    private float radius = 1f;
    [SerializeField]
    private PlayerMove playerMove;

    // Start is called before the first frame update
    void OnEnable()
    {
        HourseRB = this.gameObject.GetComponent<Rigidbody>();
    }


    // Update is called once per frame
    void Update()
    {
        if (!IfUsed) return;
        Move(HourseSpeed, GetOrient());
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!IfUsed&&other.tag=="Player")
        {
            IfEnable = true;
            playerMove.IsPlayerMove = false;
            StartCoroutine(HourseMove(other.gameObject));
            IfUsed = true;
        }
        
    }
    private void OnTriggerExit(Collider other)
    {
        IfUsed = false;
        Debug.Log("Reade to Reuse");
    }

    private string GetOrient()
    {// 右
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            return "right";
        }
        // 左
        else if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            return "left";
        }
        // 上
        else if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
        {
            return "up";
        }
        // 下
        else if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
        {
            return "down";
        }
        else return null;
        
    }
    Vector3 LastMove = Vector3.zero;
    private void Move(float speed,string orient)
    {
        // 右
        if (orient=="right"&&IfEnable)
        {
            LastMove = Vector3.right.normalized* speed;
             
        }

        // 左
        if (orient == "left" && IfEnable)
        {
            LastMove = Vector3.left.normalized * speed;
     
        }
        // 上
        if (orient == "up"&& IfEnable)
        {
            LastMove = Vector3.forward.normalized * speed;
          
        }
        // 下
        if (orient == "down" && IfEnable)
        {
            LastMove = Vector3.back.normalized * speed;
        }
        
        Debug.Log(LastMove);
        transform.position += LastMove * Time.deltaTime;
        IfEnable = false;
    }

    IEnumerator HourseMove(GameObject player)
    {
        
        player.SetActive(false);
        player.transform.SetParent(this.transform);
        Debug.Log("Parent Set");

        
        yield return new WaitForSeconds(HourseMoveTime);
        Move(0,null);
        player.SetActive(true);
        this.transform.DetachChildren();
        playerMove.IsPlayerMove = true;
        
        starttime = 0;
    }


    public void OnOff()      
    {
        IfEnable = !IfEnable;
    }
}
