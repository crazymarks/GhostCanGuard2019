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
    Max
}

public class InputManager : SingletonMonoBehavior<InputManager>
{
    /// <summary>
    /// 現在押されているボタン
    /// </summary>
    public ControllerButton CurrentControllerButton { get; private set; } = ControllerButton.Max;


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
            CurrentControllerButton = ControllerButton.A;
        if (Input.GetKeyDown(KeyCode.S))
            CurrentControllerButton = ControllerButton.B;
        if (Input.GetKeyDown(KeyCode.D))
            CurrentControllerButton = ControllerButton.X;
        if (Input.GetKeyDown(KeyCode.F))
            CurrentControllerButton = ControllerButton.Y;
    }
}
