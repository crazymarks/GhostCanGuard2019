using UnityEditor;
using UnityEngine;
public class CaptureScreenshotFromEditor : Editor
{
    [MenuItem("Edit/CaptureScreenshot #%F12")]
    private static void CaptureScreenshot()
    {
        string path = EditorUtility.SaveFilePanel("Save Screenshot", Application.dataPath, System.DateTime.Now.ToString("yyyyMMdd-HHmmss"), "png");
        UnityEngine.ScreenCapture.CaptureScreenshot(path);
        var assembly = typeof(UnityEditor.EditorWindow).Assembly;
        var type = assembly.GetType("UnityEditor.GameView");
        var gameview = EditorWindow.GetWindow(type);
        gameview.Repaint();
        Debug.Log("ScreenShot: " + path);
    }
}