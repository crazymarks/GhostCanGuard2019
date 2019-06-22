using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartManager : MonoBehaviour
{
    public GameObject desPanel;
    // Start is called before the first frame update
    void Start()
    {
        SceneManager.LoadScene(1);
    }

    public void onDesButtonDown()
    {
        desPanel.SetActive(true);
    }

    public void OnCloseButtonDown()
    {
        desPanel.SetActive(false);
    }

    public void OnQuitButtonDown()
    {
        Application.Quit();
    }
    // Update is called once per frame
    
}
