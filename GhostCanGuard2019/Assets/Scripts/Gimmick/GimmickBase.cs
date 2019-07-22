using System;
using UnityEngine;

/*
 * gimmickに継承するクラス
 * gimmickにEventを追加する関数やgimmickとカーソルが
 * 重なった時にUIを表示する関数
 */

public class GimmickBase : MonoBehaviour
{

    //ギミックの効果や説明のUIの展開
    [SerializeField] protected GameObject gimmickUIParent;
    protected stop st = null;
    // 各ギミックで必要なボタンを入れる
    [SerializeField] protected ControllerButton[] gimmickButtons = null; 

    virtual protected void Start()
    {
        //st = GameManager.Instance.GetComponent<stop>();
        
        if (gimmickUIParent == null)
        {
            gimmickUIParent = transform.Find("UI").gameObject;
        }
    }
    /// <summary>
    /// gimmickの効果を表示、非表示させる
    /// </summary>
    public void GimmickUIsOnOff(bool onoff)
    {
        // UIの展開とギミック発動のフラグが異なっていたらreturn
        if (onoff != GimmickManager.Instance.GimmickFrag) return;

        // UI表示
        gimmickUIParent.SetActive(onoff);
    }

    /// <summary>
    /// playerがギミックとカーソルが重なっているときにボタンを押した時に呼ぶ
    /// </summary>
    public void ClickGimmick()
    {
        GimmickManager.Instance.SetGimmickAction(CurrentButtonIN);
    }
    protected void CurrentButtonIN()
    {
        // 押したときのボタンをギミック処理に送る
        PushButtonGamePad(InputManager.Instance.CurrentControllerButton);
    }
    /// <summary>
    /// switch文で押されたボタンに対する処理を各ギミックで行う
    /// </summary>
    protected virtual void PushButtonGamePad(ControllerButton controller)
    {

    }
}
