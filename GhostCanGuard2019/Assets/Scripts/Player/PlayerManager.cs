using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerState
{
    Stop,
    Play,
    Slow,
}

public class PlayerManager: MonoBehaviour
{
    private PlayerState currentPlayerState;
    public PlayerState CurrentPlayer => currentPlayerState;

    public static PlayerManager Instance;

    private float playerSpeed = 0;

    private void Awake()
    {
        Instance = this;        
    }
    private void Start()
    {
        playerSpeed = PlayerMove.Instance.PlayerSpeed;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
            SetCurrentState(PlayerState.Play);
        if (Input.GetKeyDown(KeyCode.O))
            SetCurrentState(PlayerState.Stop);
        if (Input.GetKeyDown(KeyCode.I))
            SetCurrentState(PlayerState.Slow);
    }

    public void SetCurrentState(PlayerState state)
    {
        currentPlayerState = state;
        OnGameStateChanged(currentPlayerState);
    }

    private void OnGameStateChanged(PlayerState state)
    {
        switch(state)
        {
            case PlayerState.Stop:
                PlayerMove.Instance.PlayerSpeed = 0;
                break;
            case PlayerState.Play:
                PlayerMove.Instance.PlayerSpeed = playerSpeed;
                break;
            case PlayerState.Slow:
                PlayerMove.Instance.PlayerSpeed = playerSpeed / 3;
                break;
            default:
                break;
        }
    }
}
