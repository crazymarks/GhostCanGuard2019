using System;
using UnityEngine;

/*
 * gimmickに継承するクラス
 * gimmickにEventを追加する関数やgimmickクリック時にUIを表示する関数
 */

public class GimmickBase : MonoBehaviour
{

    //ギミックの効果や説明のUIの展開
    [SerializeField]
    protected GameObject gimmickUIParent;
    protected stop st;
    // 各ギミックで必要なボタンを入れる
    [SerializeField]
    protected ControllerButton[] gimmickButtons = null;

    protected bool descriptionUIOn = false; //説明文が展開されているかのフラグ　//オウカンウ

    private void Awake()
    {
        st = GameManager.Instance.GetComponent<stop>();
    }

    virtual protected void Start()
    {        
        if (gimmickUIParent == null)
        {
            gimmickUIParent = transform.Find("ButtonUICanvas").gameObject;
        }
    }
    /// <summary>
    /// gimmickの効果を表示、非表示させる
    /// </summary>
    /// <param name="onoff">true: 展開  false: 収縮</param>
    public void GimmickUIsOnOff(bool onoff)
    {
        // UIの展開とギミック発動のフラグが異なっていたらreturn
        if (onoff != GimmickManager.Instance.GimmickFrag) return;
        
        // playerの動きを止める
        //PlayerManager.Instance.SetCurrentState(PlayerState.Gimmick);
        // UI表示
        gimmickUIParent.SetActive(onoff);
        // Gimmickが発動待ち
        GimmickManager.Instance.GimmickFrag = false;
    }
    /// <summary>
    /// playerがギミックとカーソルが重なっているときにボタンを押した時に呼ぶ
    /// </summary>
    public void ClickGimmick()
    {
        if (Input.GetButtonDown("Send") || Input.GetButtonDown("Info"))  //前押し処理お避けるため加えた制限//オウカンウ
        {
            GimmickManager.Instance.SetGimmickAction(() => CurrentButtonIN());      
        }
           
        //Debug.Log("Click");
    }
    protected void CurrentButtonIN()
    {
        // 押したときのボタンをギミック処理に送る
        PushButtonGamePad(InputManager.Instance.CurrentControllerButton);
        InputManager.Instance.ClearCurrentButton();
        GimmickUIsOnOff(false);
        //Debug.Log(InputManager.Instance.CurrentControllerButton);
    }
    /// <summary>
    /// switch文で押されたボタンに対する処理を各ギミックで行う
    /// </summary>
    protected virtual void PushButtonGamePad(ControllerButton controller)
    {

    }


    /// <summary>
    /// 説明文UIの展開と収縮関数です　//オウカンウ
    /// </summary>
    /// <param name="ギミックのCSVファイル中の名前"></param>
    protected void ShowDescription(string name)
    {
        if (descriptionUIOn) return;
        LoadDescription.Instance.ShowDesc(name);
        descriptionUIOn = true;
        st.canStop = false;
        st.DescriptionPhase = true;
        gimmickUIParent.GetComponent<DescriptionUIChange>().DescriptionOnOff();
        
    }

    protected void HideDescription()
    {
        if (!descriptionUIOn) return;
        LoadDescription.Instance.HideDesc();
        descriptionUIOn = false;
        st.canStop = true;
        st.DescriptionPhase = false;
        gimmickUIParent.GetComponent<DescriptionUIChange>().DescriptionOnOff();
        GimmickManager.Instance.ClearGimmick();                 //収縮の時はギミックの登録をクリアします
    }


}
