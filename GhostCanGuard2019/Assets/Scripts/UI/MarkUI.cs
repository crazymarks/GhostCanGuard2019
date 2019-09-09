using UnityEngine;

public class MarkUI : MonoBehaviour
{
    private Vector3 parentGimmickPos = Vector3.zero;    // 親のGimmick格納
    private RectTransform rectTransformUI = null;       // UI自身のposition格納
    [SerializeField] private Vector3 plusoffset = new Vector3(2.2f, 0, 2.2f);     // UIの配置調整


    private void OnValidate()
    {
        parentGimmickPos = this.transform.root.position;    // 親のGimmick取得
        rectTransformUI = transform as RectTransform;       // UIのRectTransformを取得
        rectTransformUI.position = RectTransformUtility.WorldToScreenPoint(Camera.main, plusoffset);
    }
    private void OnEnable()
    {
        setUIPosition();
    }
    //private void Awaken()
    //{
    //    setUIPosition();
    //}
    private void Start()
    {
        setUIPosition();
    }

    public void setUIPosition()
    {
        parentGimmickPos = this.transform.root.position;    // 親のGimmick取得
        rectTransformUI = transform as RectTransform;       // UIのRectTransformを取得
        rectTransformUI.position = RectTransformUtility.WorldToScreenPoint(Camera.main,  plusoffset);
    }
}
