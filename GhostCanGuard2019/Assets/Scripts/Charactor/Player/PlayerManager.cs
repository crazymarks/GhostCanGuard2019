using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Playerの状態変化クラス
 */

public enum PlayerState
{
    Stop,       // プレイヤーが停止状態
    Play,       // 移動できる状態
    Slow,       // gimmickなどにより動きがおそkじゅなっている状態
    Gimmick,    // gimmick操作中の状態
}

public class PlayerManager: SingletonMonoBehavior<PlayerManager>
{
    private PlayerState currentPlayerState = PlayerState.Stop;
    public PlayerState CurrentPlayerState => currentPlayerState;

    private float playerSpeed = 0f;
    private float playerTurnSpeed = 0f;

    //private Animator animcontrol;
    private float nowTime = 0;      // 経過時間
    private float cancelSlow = 5;   // slowが解除される時間



    [SerializeField]
    //private PlayerMove playerMove = null;
    private PlayerControl playerControl = null;

    private void Start()
    {
        if (playerControl == null)
        {
            try
            {
                playerControl = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerControl>();
            }
            catch (System.NullReferenceException)
            {
                Debug.Log("プレイヤー未発見" + name);
            }

        }
        // playerの初期のspeedを取得する
        //playerSpeed = playerMove.PlayerSpeed;
        playerSpeed = playerControl.speed;
        playerTurnSpeed = playerControl.turnSpeed;
        //Debug.Log("playerの初期のspeedは"+playerSpeed);
        //animcontrol = playerControl.gameObject.GetComponentInChildren<Animator>();
        
    }

    private void Update()
    {
        if (currentPlayerState != PlayerState.Slow) return;
        SlowStateToPlay();
        //animcontrol.SetFloat("speed", playerControl.velocity);
        // Debug用
        //if (Input.GetKeyDown(KeyCode.P))
        //    SetCurrentState(PlayerState.Play);
        //if (Input.GetKeyDown(KeyCode.O))
        //    SetCurrentState(PlayerState.Stop);
        //if (Input.GetKeyDown(KeyCode.I))
        //    SetCurrentState(PlayerState.Slow);
    }

    /// <summary>
    /// Playerのstateを設定する
    /// </summary>
    /// <param name="state"></param>
    public void SetCurrentState(PlayerState state)
    {
        currentPlayerState = state;
        OnGameStateChanged(currentPlayerState);
    }

    /// <summary>
    /// stateの状態によってplayerの移動速度を変化変化させる
    /// </summary>
    private void OnGameStateChanged(PlayerState state)
    {
      
        switch (state)
        {
            case PlayerState.Stop:
                playerControl.speed = 0;
                playerControl.turnSpeed = 0;
                //animcontrol.speed = 0;
                break;
            case PlayerState.Play:
                playerControl.speed = playerSpeed;
                playerControl.turnSpeed = playerTurnSpeed;
                //animcontrol.speed = 1;
                //animcontrol.SetBool("gimmick", false);
                break;
            case PlayerState.Slow:
                playerControl.speed = playerSpeed / 3;
                PlayerAnimationController.Instance.SetAnimatorValue(SetPAnimator.Walk);
                //animcontrol.speed = 1 / 3;
                break;
            case PlayerState.Gimmick:
                playerControl.speed = 0;
                playerControl.turnSpeed = 0;
                //animcontrol.SetBool("gimmick", true);
                break;
            default:
                break;
        }
        
    }

    /// <summary>
    /// Slowの状態変化解除
    /// </summary>
    private void SlowStateToPlay()
    {
        nowTime += Time.deltaTime;

        if (nowTime > cancelSlow)
        {
            nowTime = 0;
            SetCurrentState(PlayerState.Play);
        }
    }
}
