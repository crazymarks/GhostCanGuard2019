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
    public float velocity { get { return rb.velocity.magnitude; } }
    //public static PlayerMove instancePM;

    private bool _playerMove = true;
    public bool CanPlayerMove { get { return _playerMove; } set { _playerMove = value; rb.velocity = Vector3.zero; } }


    float horizontal = 0;
    float vertical = 0;

    private PlayerrAnimationController animationController;

    // Start is called before the first frame update
    void Start()
    {
        rb = this.gameObject.GetComponent<Rigidbody>();
        animationController = GetComponentInChildren<PlayerrAnimationController>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        move(speed);
    }

    private void Update()
    {
        if (!_playerMove) return;

        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");

        // playerのAnimation処理
        //if (horizontal == 0 && vertical == 0)
        if (velocity < 0.1)
            animationController.SetNormalAnimation(PAnimation.Wait);
        else
            animationController.SetNormalAnimation(PAnimation.Run);
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
    
    public void SetAnimationByPlayer(PAnimation param)
    {
        animationController.PlayPlayerAnimation(param);
    }
}
