using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
/*----------------------------------------------------------------
 * GimmickManager
 * ギミックごとに定められている関数をAction型のGimmickFuncに登録し
 * GimmickFuncをタイミングごとに呼び出す。
 ----------------------------------------------------------------*/


public class GimmickManager : SingletonMonoBehavior<GimmickManager>
{
    // gimmickそれぞれにつける
    private System.Action GimmickFunc = null;
    
    private List<string> gimmickNames = new List<string>();

    /// <summary>
    /// 関数をsetする
    /// </summary>
    /// <param name="action">実行したい関数</param>
    public void SetGimmickAction(System.Action action)
    {
        GimmickFunc = action;
        gimmickNames.Add(action.Method.Name);
    }
    /// <summary>
    /// Action初期化
    /// </summary>
    public void ClearGimmick()
    {
        if (gimmickNames != null)
        {
            gimmickNames.ForEach(name => Debug.Log(name));
            gimmickNames.Clear();
        }
        GimmickFunc = null;
    }

    private void Update()
    {
        if (GimmickFunc == null) return;
        GimmickFunc();
    }

}
