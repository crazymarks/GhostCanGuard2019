using UnityEngine;

/* 
 ** 外部スクリプトで呼ぶクラス
 ** ギミックにマウスクリックが起きた時に
 ** ギミック効果をUIとして展開する関数をもつ 
 */

public class GimickOpen : MonoBehaviour
{
    // hierarchy上にあるギミック
    [SerializeField]
    private GameObject gimmickObject;

    // UIの親の空オブジェ
    private GameObject gimmickUIParent;

    private void Start()
    {
        gimmickUIParent = gimmickObject.transform.GetChild(0).gameObject;
    }

    /// <summary>
    /// gimmickの効果を表示、非表示させる
    /// </summary>
    /// <param name="onoff">true: 展開  false: 収縮</param>
    public void GimmickUIsOnOff(bool onoff)
    {
        // UI表示
        gimmickUIParent.SetActive(onoff);
    }
}
