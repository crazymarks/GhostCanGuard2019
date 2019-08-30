using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class EndingSceneManager : MonoBehaviour
{
    Button Restart;
    Button NextStage;
    Button TitleScene;
   
    List<Button> EndingSelects = new List<Button>();
    Dictionary<string, Button> EndindSelectMap;
    
    Button selectedButton;
    int selectButtonIndex;
    public Image SelectArray;
    // Start is called before the first frame update
    void Start()
    {
        foreach (var button in GetComponentsInChildren<Button>())
        {
            EndingSelects.Add(button);
            //EndindSelectMap.Add(button.name, button);
        }
        selectedButton = EndingSelects[0];
        SelectArray.rectTransform.position = selectedButton.GetComponent<RectTransform>().position - new Vector3(200, 0);
        SelectArray.transform.SetParent(selectedButton.transform);
    }

    // Update is called once per frame
    bool changed = false;
    void Update()
    {
        if (Input.GetButtonDown("Up"))
        {
            selectButtonIndex = (selectButtonIndex + 1) % EndingSelects.Count;
            selectedButton = EndingSelects[selectButtonIndex];
            SelectArray.rectTransform.position = selectedButton.GetComponent<RectTransform>().position - new Vector3(200, 0);
            SelectArray.transform.SetParent(selectedButton.transform);
            changed = true;
        }
        if (Input.GetButtonDown("Down"))
        {
            selectButtonIndex = (selectButtonIndex + EndingSelects.Count - 1) % EndingSelects.Count;
            selectedButton = EndingSelects[selectButtonIndex];
            SelectArray.rectTransform.position = selectedButton.GetComponent<RectTransform>().position - new Vector3(200, 0);
            SelectArray.transform.SetParent(selectedButton.transform);
            changed = true;
        }
        if (Input.GetAxisRaw("Down") != 0)
        {
            if (changed) return;
            if (Input.GetAxisRaw("Down") <= 0 )
            {
                selectButtonIndex = (selectButtonIndex + 1) % EndingSelects.Count;
                selectedButton = EndingSelects[selectButtonIndex];
                SelectArray.rectTransform.position = selectedButton.GetComponent<RectTransform>().position - new Vector3(200, 0);
                SelectArray.transform.SetParent(selectedButton.transform);
                changed = true;
            }
            else
            {
                //selectLastButton(selectedButton, selectButtonIndex, EndingSelects);
                selectButtonIndex = (selectButtonIndex + EndingSelects.Count - 1) % EndingSelects.Count;
                selectedButton = EndingSelects[selectButtonIndex];
                SelectArray.rectTransform.position = selectedButton.GetComponent<RectTransform>().position - new Vector3(200, 0);
                SelectArray.transform.SetParent(selectedButton.transform);
                changed = true;
            }
        }
        if (changed && Input.GetAxisRaw("Down") == 0)
        {
            changed = false;
        }
        if (Input.GetButtonDown("Send"))
        {
            selectedButton.onClick.Invoke();
        }
    }

    void selectNextButton(Button selector, int buttonIndex, List<Button> buttons)
    {
        buttonIndex = (buttonIndex + 1) % buttons.Count;
        selector = buttons[buttonIndex];
    }
    void selectLastButton(Button selector, int buttonIndex, List<Button> buttons)
    {
        buttonIndex = (buttonIndex + buttons.Count - 1) % buttons.Count;
        selector = buttons[buttonIndex];
    }
}
