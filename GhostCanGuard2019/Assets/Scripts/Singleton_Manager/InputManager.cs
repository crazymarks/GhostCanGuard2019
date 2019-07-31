using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// なんのボタンが押されたか判定用
/// </summary>
public enum ControllerButton
{
    A,
    B,
    X,
    Y,
<<<<<<< HEAD
    Max
=======
    Max,
    Null
>>>>>>> origin/wangguanyu
}

public class InputManager : SingletonMonoBehavior<InputManager>
{
    /// <summary>
    /// 現在押されているボタン
    /// </summary>
    public ControllerButton CurrentControllerButton { get; private set; } = ControllerButton.Max;


    private void Update()
    {
<<<<<<< HEAD
        if (Input.GetKeyDown(KeyCode.A))
            CurrentControllerButton = ControllerButton.A;
        if (Input.GetKeyDown(KeyCode.S))
            CurrentControllerButton = ControllerButton.B;
        if (Input.GetKeyDown(KeyCode.D))
            CurrentControllerButton = ControllerButton.X;
        if (Input.GetKeyDown(KeyCode.F))
            CurrentControllerButton = ControllerButton.Y;
=======
        if (Input.GetKeyDown(KeyCode.K) || Input.GetButtonDown("Cancel"))
            CurrentControllerButton = ControllerButton.A;
        if (Input.GetKeyDown(KeyCode.L) || Input.GetButtonDown("Send"))
        {
            CurrentControllerButton = ControllerButton.B;
            //Debug.Log("Send");
        }
            
        
        if (Input.GetKeyDown(KeyCode.J))
            CurrentControllerButton = ControllerButton.X;
        if (Input.GetKeyDown(KeyCode.I) || Input.GetButtonDown("Info"))
            CurrentControllerButton = ControllerButton.Y;
        //else
        //    CurrentControllerButton = ControllerButton.Null;
>>>>>>> origin/wangguanyu
    }
}
