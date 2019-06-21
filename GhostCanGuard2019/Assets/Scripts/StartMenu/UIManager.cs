using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    //单例模式
    private static UIManager _instance;
    public static UIManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<UIManager>();
            }
            return _instance;
        }
    }

    public GameObject menuPanel;//菜单面板
    public GameObject desPanel;//介绍面板

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ShowMenuPanel();
        }
    }

    //显示菜单面板
    private void ShowMenuPanel()
    {
        menuPanel.SetActive(true);
    }

    //按下是按钮
    public void OnYButtonDown()
    {
        SceneManager.LoadScene(0);
    }

    //按下否按钮
    public void OnNButtonDown()
    {
        menuPanel.SetActive(false);
    }

    //显示介绍面板
    public void ShowDesPanel(string str)
    {
        desPanel.SetActive(true);
        desPanel.transform.GetChild(0).GetComponent<Text>().text = str;
    }

    //按下关闭按钮
    public void OnCloseButtonDown()
    {
        desPanel.SetActive(false);
    }
}
