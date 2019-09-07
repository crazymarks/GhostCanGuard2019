using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public struct ScreenOffset
{
    public float left, top, right, bottom;

    public ScreenOffset(float _left, float _top, float _right, float _bottom)
    {
        left = _left;
        top = _top;
        right = _right;
        bottom = _bottom;
    }
}

public class DesceriptionAnimation : MonoBehaviour
{
    public Image Bracket;
    public Image Description;
    public RectTransform CursorPosition;
    public float timeToFinish = 2f;
    float unitPerFrame;
    bool startMove = false;
    float startTime;

    public ScreenOffset offset = new ScreenOffset(0,0,0,0);

    Vector2 offsetMax;
    Vector2 offsetMin;

    //private void OnEnable()
    //{
    //    Description.enabled = false;
    //}

    // Start is called before the first frame update
    void Start()
    {
        //Bracket.rectTransform.offsetMax = new Vector2(-Screen.width / 2, -Screen.height / 2) + CursorPosition.offsetMax; //画面の右上から中心へのベクトル + 中心からカーソルの右上へのベクトル
        //Bracket.rectTransform.offsetMin = new Vector2(Screen.width / 2, Screen.height / 2) + CursorPosition.offsetMin;　//画面の左下から中心へのベクトル + 中心からカーソルの左下へのベクトル
        startTime = 0;
        offsetMax = new Vector2(-offset.right, -offset.top);
        offsetMin = new Vector2(offset.left, offset.bottom);
        unitPerFrame = 1 / timeToFinish;

    }

    // Update is called once per frame
    void Update()
    {
        if (startMove)
        {
            Anim();
        }
        
    }


    void Anim()
    {
        Bracket.rectTransform.offsetMax = Vector2.Lerp(Bracket.rectTransform.offsetMax, offsetMax, unitPerFrame * startTime);
        Bracket.rectTransform.offsetMin = Vector2.Lerp(Bracket.rectTransform.offsetMin, offsetMin, unitPerFrame * startTime);
        startTime += Time.unscaledDeltaTime;
        if (startTime >= timeToFinish)
        {
            startMove = false;
        }
        if ((Bracket.rectTransform.offsetMax- offsetMax).sqrMagnitude < 1 && !Description.enabled)
        {
            Description.enabled = true;
        }
    }

    public void trigger()
    {
        if (!startMove)
        {
            Debug.Log(Bracket.rectTransform.offsetMax);
            Debug.Log(Bracket.rectTransform.offsetMin);
            Debug.Log(CursorPosition.offsetMax);
            Debug.Log(CursorPosition.offsetMin);
            Debug.Log(CursorPosition.gameObject.transform.position);
            startTime = 0;
            startMove = true;
        }
    }
    

    public void reset()
    {
        Description.enabled = false;
        startMove = false;
        Bracket.rectTransform.offsetMax = new Vector2(-Screen.width / 2, -Screen.height / 2) + CursorPosition.offsetMax; //画面の右上から中心へのベクトル + 中心からカーソルの右上へのベクトル
        Bracket.rectTransform.offsetMin = new Vector2(Screen.width / 2, Screen.height / 2) + CursorPosition.offsetMin;　//画面の左下から中心へのベクトル + 中心からカーソルの左下へのベクトル
    }


    private void OnValidate()
    {
        Bracket.rectTransform.offsetMax = new Vector2(-offset.right, -offset.top);
        Bracket.rectTransform.offsetMin = new Vector2(offset.left, offset.bottom);
    }
    public void generate(Sprite bracket,Sprite description)
    {
        Bracket.sprite = bracket;
        Description.sprite = description;
    }
    public void generate(Sprite bracket, Sprite description,RectTransform cursor)
    {
        Bracket.sprite = bracket;
        Description.sprite = description;
        CursorPosition = cursor;
    }
    public void generate(Sprite bracket, Sprite description, RectTransform cursor ,float _timetofinish)
    {
        Bracket.sprite = bracket;
        Description.sprite = description;
        CursorPosition = cursor;
        timeToFinish = _timetofinish;
    }
}
