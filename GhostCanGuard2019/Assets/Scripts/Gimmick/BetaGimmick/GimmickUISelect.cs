using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * ギミックのUIにつけるスクリプト
 * public関数をUIのそれぞれのボタンで呼び出す
 */

public class GimmickUISelect : MonoBehaviour
{
    /// <summary>
    /// Cancelボタンを押したときの処理
    /// </summary>
    public void OnCancel()
    {
        this.gameObject.SetActive(false);
        Debug.Log("cancel");
    }

    /// <summary>
    /// ギミックの効果を発動するボタンを押したときの処理
    /// </summary>
    public void GimmickStartOne()
    {
        Debug.Log("GimmickStart");
    }

    public void GimmickStartTwo()
    {
        Debug.Log("GimmickStart");
    }
}
