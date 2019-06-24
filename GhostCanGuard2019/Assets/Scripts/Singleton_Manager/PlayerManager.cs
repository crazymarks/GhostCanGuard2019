﻿using System.Collections;
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
    public PlayerState CurrentPlayer => currentPlayerState;
    
    private float playerSpeed = 0;

    [SerializeField]
    private PlayerMove playerMove = null;
   
    private void Start()
    {
        // playerの初期のspeedを取得する
        playerSpeed = playerMove.PlayerSpeed;
    }

    private void Update()
    {
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
        switch(state)
        {
            case PlayerState.Stop:
                playerMove.PlayerSpeed = 0;
                break;
            case PlayerState.Play:
                playerMove.PlayerSpeed = playerSpeed;
                break;
            case PlayerState.Slow:
                playerMove.PlayerSpeed = playerSpeed / 3;
                break;
            case PlayerState.Gimmick:
                playerMove.PlayerSpeed = 0;
                break;
            default:
                break;
        }
    }
}