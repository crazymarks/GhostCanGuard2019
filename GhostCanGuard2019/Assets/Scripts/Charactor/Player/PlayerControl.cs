﻿using UnityEngine;

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

    //animation
    public PlayerAnimationController playerAnim;


    /// <summary>
    /// StopFX
    /// </summary>
    public GameObject StopFX;

    float horizontal = 0;
    float vertical = 0;

    // Start is called before the first frame update
    void Start()
    {
        rb = this.gameObject.GetComponent<Rigidbody>();
        if(playerAnim == null)
        {
            playerAnim = GetComponentInChildren<PlayerAnimationController>();
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        move(speed);
    }

    private void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");

        // playerのAnimation処理
        //if (horizontal == 0 && vertical == 0)
        if (velocity < 0.1)
            playerAnim.SetNormalAnimation(PAnimation.Wait);
            
        //else if (PlayerManager.Instance.CurrentPlayerState == PlayerState.Slow)
        //    playerAnim.SetNormalAnimation(PAnimation.Walk);
        else
            playerAnim.SetNormalAnimation(PAnimation.Run);


        FXcheck();
    }
    
    /// <summary>
    /// FXcheck
    /// </summary>
    void FXcheck()
    {
        if (StopSystem.Instance.stopped && !StopFX.activeSelf)
        {
            StopFX.SetActive(true);
        }
        if (!StopSystem.Instance.stopped && StopFX.activeSelf)
        {
            StopFX.SetActive(false);
        }
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
