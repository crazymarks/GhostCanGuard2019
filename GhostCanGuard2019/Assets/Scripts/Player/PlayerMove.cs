using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    // playerの速さ
    [SerializeField]
    private float pMove = 5.0f; 

    private Rigidbody playerRB;

    public static PlayerMove instancePM;

    private bool _playerMove = true;
    public bool IsPlayerMove { get { return _playerMove; } set { _playerMove = value; } }

    private float radius = 0.5f;

    // Start is called before the first frame update
    void Start()
    {
        playerRB = this.gameObject.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        PlayerOperation(pMove);
    }
   
    private void PlayerOperation(float speed)
    {
        if (!_playerMove) return;

        // キー操作がないときmoveを０にする
        Vector3 move = Vector3.zero;

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
        float movedistance = (speed / Mathf.Sqrt(2.0f) * Time.deltaTime);
        RaycastHit hit;
        //自身の位置から移動方向に自身の半径+移動距離分の長さのRayを飛ばす
        if (Physics.Raycast(transform.position, move, out hit, movedistance + radius, 1))
        {
            Debug.Log(hit.point);
            //移動距離をClampして移動距離を制限する
            movedistance = Mathf.Clamp(movedistance, 0, hit.distance - radius > 0 ? hit.distance - radius : 0);
        }

        //transform.positionを変更して移動する
        transform.position += (move.normalized * movedistance);

    }
}
