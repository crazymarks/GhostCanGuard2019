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
            SwapSpriteState(button, ButtonState.None);
        }
        selectButtonIndex = 0;
        selectedButton = EndingSelects[selectButtonIndex];
        SelectArray.rectTransform.position = selectedButton.GetComponent<RectTransform>().position - new Vector3(200, 0);
        SelectArray.transform.SetParent(selectedButton.transform);
        SwapSpriteState(selectedButton, ButtonState.Stay);
    }

    // Update is called once per frame
    bool changed = false;
    void Update()
    {
        if (Input.GetButtonDown("Up"))
        {
            selectLastButton();
            changed = true;
        }
        if (Input.GetButtonDown("Down"))
        {
            selectNextButton();
            changed = true;
        }
        if (Input.GetAxisRaw("Vertical") != 0)
        {
            if (changed) return;
            if (Input.GetAxisRaw("Vertical") <= 0 )
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
        if (changed && Input.GetAxisRaw("Vertical") == 0)
        {
            changed = false;
        }
        if (Input.GetButtonDown("Send"))
        {
            SwapSpriteState(selectedButton, ButtonState.Release);
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
        SwapSpriteState(selectedButton, ButtonState.None);
        selectButtonIndex = (selectButtonIndex + 1) % EndingSelects.Count;
        selectedButton = EndingSelects[selectButtonIndex];
        SelectArray.rectTransform.position = selectedButton.GetComponent<RectTransform>().position - new Vector3(200, 0);
        SelectArray.transform.SetParent(selectedButton.transform);
        SwapSpriteState(selectedButton, ButtonState.Stay);
        Debug.Log("last");
    }
    void selectLastButton()
    {
        Debug.Log("next");
        SwapSpriteState(selectedButton, ButtonState.None);
        selectButtonIndex = (selectButtonIndex + EndingSelects.Count - 1) % EndingSelects.Count;
        selectedButton = EndingSelects[selectButtonIndex];
        SelectArray.rectTransform.position = selectedButton.GetComponent<RectTransform>().position - new Vector3(200, 0);
        SelectArray.transform.SetParent(selectedButton.transform);
        SwapSpriteState(selectedButton, ButtonState.Stay);
    }
    public void SwapSpriteState(Button button, ButtonState buttonState)
    {
        switch (buttonState)
        {
            //デフォルト
            case ButtonState.None:
                button.GetComponent<Image>().sprite = button.spriteState.disabledSprite;
                break;
            //選択されて
            case ButtonState.Stay:
                button.GetComponent<Image>().sprite = button.spriteState.highlightedSprite;
                break;
            //押されて
            case ButtonState.Release:
                button.GetComponent<Image>().sprite = button.spriteState.pressedSprite;
                break;
            default:
                break;
        }

    }
}
