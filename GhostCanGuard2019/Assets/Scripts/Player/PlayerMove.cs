//using System.Collections;
//using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    // playerの速さ
    [SerializeField]
    private float speed = 5.0f; 
    public float PlayerSpeed { get { return speed; } set { speed = value; } }

    //private Rigidbody playerRB;

    public static PlayerMove Instance;
    
    private float radius = 1.0f;

    [SerializeField]
    private LayerMask DontEnter;

    private Vector3 debugPos;
    float movedistance = 0;

    private void Awake()
    {
        Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        //playerRB = this.gameObject.GetComponent<Rigidbody>();
        debugPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        PlayerOperation();
    }
   
    private void PlayerOperation()
    {
        if (PlayerManager.Instance.CurrentPlayer == PlayerState.Stop) return;

        // キー操作がないときmoveを０にする
        Vector3 move = Vector3.zero;

        if(debugPos != transform.position)
        {
            Debug.Log(movedistance);
        }

        debugPos = transform.position;



        // 右
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            move += Vector3.right * speed;
        }
            
        // 左
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            move += Vector3.left * speed;
        }
        // 上
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
        {
            move += Vector3.forward * speed;
        }
        // 下
        if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
        {
            move += Vector3.back * speed;
        }

        //移動距離を計算する
        movedistance = (speed / Mathf.Sqrt(2.0f) * Time.deltaTime);
        RaycastHit hit;
        //自身の位置から移動方向に自身の半径+移動距離分の長さのRayを飛ばす
        if (Physics.Raycast(transform.position, move, out hit, movedistance + radius, DontEnter))
        {
            //Debug.Log(hit.point);
            //移動距離をClampして移動距離を制限する
            movedistance = Mathf.Clamp(movedistance, 0, hit.distance - radius > 0 ? hit.distance - radius : 0);
        }

        //transform.positionを変更して移動する
        transform.position += (move.normalized * movedistance);

    }
}
