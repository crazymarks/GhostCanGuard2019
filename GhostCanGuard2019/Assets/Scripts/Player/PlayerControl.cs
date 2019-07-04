using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerControl : MonoBehaviour
{
    // playerの速さ
    public float speed = 5.0f;
    public float turnSpeed = 1f;

    private Rigidbody rb;

    //public static PlayerMove instancePM;

    private bool _playerMove = true;
    public bool CanPlayerMove { get { return _playerMove; } set { _playerMove = value; } }

    private float horizontal = 0;
    private float vertical = 0;


    // Start is called before the first frame update
    void Start()
    {
        rb = this.gameObject.GetComponent<Rigidbody>();
    }

    private void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");

        // playerのAnimation処理
        if (horizontal == 0 && vertical == 0)
            PlayerAnimationController.Instance.SetAnimatorValue(SetPAnimator.Stop);
        else
            PlayerAnimationController.Instance.SetAnimatorValue(SetPAnimator.Run);
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        move(speed);
    }

    private void move(float speed)
    {
        if (!_playerMove) {
            //rb.velocity = Vector3.zero;
            return;
        }
        
        Vector3 move = new Vector3(horizontal, 0, vertical).normalized;
        

        rb.velocity = move * speed;
        //Debug.Log("プレイヤーのスピードは"+speed+"です");
        if (move!= Vector3.zero)
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(move, Vector3.up), turnSpeed);
            
    }
}
