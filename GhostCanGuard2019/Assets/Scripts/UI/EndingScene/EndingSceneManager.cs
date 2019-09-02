using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class EndingSceneManager : MonoBehaviour
{
    
   
    List<Button> EndingSelects = new List<Button>();
    //Dictionary<string, Button> EndindSelectMap;
    
    Button selectedButton;  //今選択しているボタン
    int selectButtonIndex;  //今選択しているボタンのIndex番号
    public Image SelectArray;
    // Start is called before the first frame update
    void Start()
    {
        foreach (var button in GetComponentsInChildren<Button>())
        {
            EndingSelects.Add(button);
            //EndindSelectMap.Add(button.name, button);
        }
        selectButtonIndex = 0;
        selectedButton = EndingSelects[selectButtonIndex];
        SelectArray.rectTransform.position = selectedButton.GetComponent<RectTransform>().position - new Vector3(200, 0);
        SelectArray.transform.SetParent(selectedButton.transform);
    }

    // Update is called once per frame
    bool changed = false;
    void Update()
    {
        if (Input.GetButtonDown("Up"))
        {
            selectNextButton();
            changed = true;
        }
        if (Input.GetButtonDown("Down"))
        {
            selectLastButton();
            changed = true;
        }
        if (Input.GetAxisRaw("Down") != 0)
        {
            if (changed) return;
            if (Input.GetAxisRaw("Down") <= 0 )
            {
                selectNextButton();
                changed = true;
            }
            else
            {
                selectLastButton();
                changed = true;
            }
        }
        if (changed && Input.GetAxisRaw("Down") == 0)
        {
            changed = false;
        }
        if (Input.GetButtonDown("Send"))
        {
            switch (selectedButton.name)
            {
                case "Restart":
                    LoadScene.reloadCurrent();
                    break;
                case "Next":
                    LoadScene.loadNext();
                    break;
                case "Title":
                    LoadScene.loadScene("TitleScene");
                    break;
                default:
                    break;
            }
        }
    }

    void selectNextButton()
    {
        selectButtonIndex = (selectButtonIndex + 1) % EndingSelects.Count;
        selectedButton = EndingSelects[selectButtonIndex];
        SelectArray.rectTransform.position = selectedButton.GetComponent<RectTransform>().position - new Vector3(200, 0);
        SelectArray.transform.SetParent(selectedButton.transform);
        Debug.Log("last");
    }
    void selectLastButton()
    {
        Debug.Log("next");
        selectButtonIndex = (selectButtonIndex + EndingSelects.Count - 1) % EndingSelects.Count;
        selectedButton = EndingSelects[selectButtonIndex];
        SelectArray.rectTransform.position = selectedButton.GetComponent<RectTransform>().position - new Vector3(200, 0);
        SelectArray.transform.SetParent(selectedButton.transform);
    }
}
