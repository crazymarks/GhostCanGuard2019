using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ButtonState
{
    None,
    Stay,
    Release,
}

public class ButtonManager : MonoBehaviour
{
    public static ButtonManager Instance;
    private bool debugFlag;

    private ButtonState currentButtonState = ButtonState.None;
    public ButtonState CurrentButton => currentButtonState;

    private void Awake()
    {
        Instance = this;
        debugFlag = true;
    }
    private void Update()
    {
        if ( !GimmickBase.GimmickFlag ) return;
        if(debugFlag)
        {
            Debug.Log("aaaaaaa");
            debugFlag = false;
        }
        if (Input.GetMouseButton(0))
            currentButtonState = ButtonState.Stay;
        else
            currentButtonState = ButtonState.None;

        if (Input.GetMouseButtonUp(0))
            currentButtonState = ButtonState.Release;
    }

    public void ChangeButtonState(ButtonState state)
    {
        currentButtonState = state;
    }
}
