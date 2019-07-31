using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;

public class TestGimmick : GimmickBase
{
    protected override void Start()
    {
        base.Start();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
            ClickGimmick();
        else
            GimmickManager.Instance.ClearGimmick();
    }

    /// <summary>
    /// それぞれのボタンが押された時の処理を書く
    /// </summary>
    protected override void PushButtonGamePad(ControllerButton controller)
    {
        //for(int i = 0; i < gimmickButtons.Length; i++)
        //{
        //    if (controller == gimmickButtons[i])
        //    {
        //        Debug.Log("Push " + gimmickButtons[i]);
        //        return;
        //    }
        //}

        Debug.Log("this Gimmick don't setup Button");
        //switch(controller)
        //{
        //    case ControllerButton.A:
        //        Debug.Log("Push A button! ");
        //        break;
        //    case ControllerButton.B:
        //        Debug.Log("Push B button! ");
        //        break;
        //    case ControllerButton.X:
        //        Debug.Log("Push X button! ");
        //        break;
        //    case ControllerButton.Y:
        //        Debug.Log("Push Y button! ");
        //        break;
        //}
    }
}
