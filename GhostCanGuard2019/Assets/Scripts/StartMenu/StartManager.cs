using UnityEngine;
using UnityEngine.SceneManagement;

public class StartManager : MonoBehaviour
{
    public GameObject desPanel;//介绍面板

    //按下开始按钮
    public void OnStartButtonDown()
    {
        SceneManager.LoadScene(1);
    }

    //按下介绍按钮
    public void OnDesButtonDown()
    {
        desPanel.SetActive(true);
    }

    //按下关闭按钮
    public void OnCloseButtonDown()
    {
        desPanel.SetActive(false);
    }

    //按下退出按钮
    public void OnQuitButtonDown()
    {
        Application.Quit();
    }
}
