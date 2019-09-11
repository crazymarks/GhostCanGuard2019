using UnityEngine.UI;
using UnityEngine;

public class UIManager : SingletonMonoBehavior<UIManager>
{
    public GameObject menuPanel;
    public GameObject desPanel;
    public Text ScreenMessageTextBox;


    private void Start()
    {
        desPanel.SetActive(false);
    }
    // Update is called once per frame
    //private void Update()
    //{
    //    if (Input.GetKeyDown(KeyCode.Escape))
    //    {
    //        ShowMenuPanel();
    //    }
    //}

    private void ShowMenuPanel()
    {
        menuPanel.SetActive(true);
    }

    //public void OnYButtonDown()
    //{
    //    SceneManager.LoadScene(0);
    //}
    //public void OnNButtonDown()
    //{
    //    menuPanel.SetActive(false);
    //}
    public void ShowDesPanel(string str)
    {
        desPanel.SetActive(true);
        ScreenMessageTextBox.text = str;
    }

    //public void OnCloseButtonDown()
    //{
    //    desPanel.SetActive(false);
    //}

}

