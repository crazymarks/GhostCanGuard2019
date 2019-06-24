using UnityEngine;
using UnityEditor;

public class SetUpGimmickParent :EditorWindow
{
    private GameObject gimmickEmpty;
    private GameObject gimmickPrefab;
    private GameObject gimmickUI;

    [MenuItem("GimmickBase/Create Base")]
    static void Init()
    {
        EditorWindow.GetWindow<SetUpGimmickParent>(true, "Create Base");
    }

    private void OnGUI()
    {
        try
        {
            // 空のゲームオブジェクト
            gimmickEmpty = EditorGUILayout.ObjectField("gimmickEmpty", gimmickEmpty, typeof(GameObject), true) as GameObject;
            // ギミックのモデル
            gimmickPrefab = EditorGUILayout.ObjectField("gimmickPrefab", gimmickPrefab, typeof(GameObject), true) as GameObject;
            // ギミックにつけるUIのprefab
            gimmickUI = EditorGUILayout.ObjectField("gimmickUI", gimmickUI, typeof(GameObject), true) as GameObject;

            if (GUILayout.Button("SetUp")) SetUp();
        }
        catch (System.FormatException)
        {
            Debug.Log("Create Error");
        }
    }

    private void SetUp()
    {
        gimmickPrefab.transform.parent = gimmickEmpty.transform;
        gimmickPrefab.transform.parent = gimmickEmpty.transform;
    }
}
