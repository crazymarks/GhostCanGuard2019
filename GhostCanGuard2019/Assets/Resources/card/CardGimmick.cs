using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardGimmick : GimmickBase
{
    public string CardName;
    // Start is called before the first frame update
    void Start()
    {
        _start();
    }

    // Update is called once per frame
    void Update()
    {
        if (gimmickUIParent.activeSelf)                              //UIが既に展開している場合
        {
            if (st.selectedObject != gameObject || st.SecondPhase)       //セレクトされていない　または　方向選択段階にいる場合
            {
                gimmickUIParent.SetActive(false);     //UIを収縮
            }
           
            gimmickUIParent.GetComponent<DescriptionUIChange>().ActionUIHide();
            
            


        }
        else                                                                    // UIが展開していない場合
        {
            if (st.selectedObject == gameObject && !st.SecondPhase)    //セレクトされたら、且つ、方向選択段階じゃない場合
            {
                gimmickUIParent.SetActive(true);                                   //UIを展開
                
                gimmickUIParent.GetComponent<DescriptionUIChange>().ActionUIHide();
            }
        }
    }

    protected override void PushButtonGamePad(ControllerButton controller)
    {
        base.PushButtonGamePad(controller);
        switch (controller)
        {
            case ControllerButton.A:
                if (descriptionUIOn)
                {
                    HideDescription();
                }
                break;
            case ControllerButton.B:
                break;
            case ControllerButton.X:
                break;
            case ControllerButton.Y:
                if (!descriptionUIOn)
                {
                    ShowDescription(CardName);
                }
                break;
            case ControllerButton.Max:
                break;
            default:
                break;
        }
    }
}
