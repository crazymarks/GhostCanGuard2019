using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GimmickUI : MonoBehaviour
{
    private Vector3 parentGimmickPos = Vector3.zero;    // 親のGimmick格納
    private RectTransform rectTransformUI = null;       // UI自身のposition格納
    [SerializeField] private Vector3 offset = new Vector3(-2.2f, 0, 2.2f);     // UIの配置調整


    private void OnValidate()
    {
        parentGimmickPos = this.transform.root.position;    // 親のGimmick取得
        rectTransformUI = transform as RectTransform;       // UIのRectTransformを取得
        rectTransformUI.position = RectTransformUtility.WorldToScreenPoint(Camera.main, parentGimmickPos + offset);
    }
    private void OnEnable()
    {
        parentGimmickPos = this.transform.root.position;    // 親のGimmick取得
        rectTransformUI = transform as RectTransform;       // UIのRectTransformを取得
        rectTransformUI.position = RectTransformUtility.WorldToScreenPoint(Camera.main, parentGimmickPos + offset);
    }
    private void Awaken()
    {
        parentGimmickPos = this.transform.root.position;    // 親のGimmick取得
        rectTransformUI = transform as RectTransform;       // UIのRectTransformを取得
        rectTransformUI.position = RectTransformUtility.WorldToScreenPoint(Camera.main, parentGimmickPos + offset);
    }
    private void Start()
    {
        parentGimmickPos = this.transform.root.position;    // 親のGimmick取得
        rectTransformUI = transform as RectTransform;       // UIのRectTransformを取得
        rectTransformUI.position = RectTransformUtility.WorldToScreenPoint(Camera.main, parentGimmickPos + offset);
    }
}