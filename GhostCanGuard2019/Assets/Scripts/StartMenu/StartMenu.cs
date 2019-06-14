using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartMenu : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        select.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public Canvas startMenu;
    public Canvas select;

    public void enableSelect()
    {
        startMenu.enabled=false;
        select.enabled = true;

    }

}
