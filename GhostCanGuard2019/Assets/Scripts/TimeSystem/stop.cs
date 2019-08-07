using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class stop : MonoBehaviour
{
    public LayerMask mask;
    [SerializeField]
    OutLineCamera outline;
    [SerializeField]
    private Color HighlightColor = Color.green;
    public Outline.Mode HighlightMode = Outline.Mode.OutlineAll;
    [Range(0f,10f)]
    public float HighlightWidth = 5f;
    public GameObject selectedObject;
    private GameObject outlineObject;
    [SerializeField]
    float speed = 10f;
    
    public GameObject cursor;
   
    public bool SecondPhase { get; private set; }
    //public Slider AimSlider;
    public Sprite cursor_first;
    public Sprite cursor_second;
    
    bool stopped = false;
   
    // Start is called before the first frame update
    void Start()
    {
        SecondPhase = false;
        outline = Camera.main.GetComponent<OutLineCamera>();
        outline.enabled = false;
        cursor.SetActive (false);
        stopped = false;
        cursor.GetComponent<Image>().sprite = cursor_first;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Stop") && !SecondPhase && GameManager.Instance.gameStart)
        {
            gamestop();
        }
        if (stopped)
        {
            float horizontal = Input.GetAxisRaw("Horizontal");
            float vertical = Input.GetAxisRaw("Vertical");
            Vector2 move = new Vector2(horizontal, vertical);

            cursor.transform.Translate(move * speed);
            DragRangeLimit(cursor.transform);
            if (!SecondPhase)
            {
                getObjectAtPosition();
            }
            if (outlineObject)
            {
                if (outlineObject.GetComponent<Outline>() != null && outlineObject != selectedObject)
                {
                    Destroy(outlineObject.GetComponent<Outline>());
                }
                if(outlineObject==selectedObject && selectedObject.GetComponent<Outline>() == null)
                {
                    addSingleOutline(HighlightMode, HighlightColor, HighlightWidth);
                }
            }
            outlineObject = selectedObject;
            PrepareGimmick(selectedObject);
        }
        //if (SecondPhase && !AimSlider.gameObject.activeSelf)
        //{
        //    AimSlider.gameObject.SetActive(true);
        //}
       
    }
    public void gamestop()
    {
        if (stopped)
        {
            gameStopEnd();
        }
        else
        {
            gameStopStart();
        }
    }
    void gameStopStart()
    {
        cursor.SetActive(true);
        cursor.transform.position = Camera.main.WorldToScreenPoint(GameManager.Instance.pc.gameObject.transform.position);
        Time.timeScale = 0.1f;
        outline.enabled = true;
        PlayerAnimationController.Instance.SetAnimatorValue(SetPAnimator.Hold);
        stopped = true;
        GameManager.Instance.pc.CanPlayerMove = false;
    }
    void gameStopEnd()
    {
        Time.timeScale = 1;
        cursor.SetActive(false);
        outline.enabled = false;
        PlayerAnimationController.Instance.SetAnimatorValue(SetPAnimator.Push);
        stopped = false;
        GameManager.Instance.pc.CanPlayerMove = true;
        if (outlineObject && outlineObject.GetComponent<Outline>() != null)
            Destroy(outlineObject.GetComponent<Outline>());
        outlineObject = null;
        SecondPhase = false;
        cursor.GetComponent<Image>().sprite = cursor_first;
        selectedObject = null;
    }

    public void getObjectAtPosition()
    {
        Ray ray = Camera.main.ScreenPointToRay(cursor.transform.position);

        RaycastHit hit;
        if (Physics.Raycast(ray, out hit,mask) && hit.collider.gameObject != selectedObject)
        {
            if (hit.collider.tag == "Gimmik")
            {
                //EventSystem.current.SetSelectedGameObject(hit.collider.gameObject);
                //gimmickmanager.instance.GetGimick = hit.collidr.gamaobject;
                Debug.Log(hit.collider.gameObject.name);
                selectedObject = hit.collider.gameObject;
            }
            else if (hit.collider.tag == "Player")
            {
                selectedObject = hit.collider.gameObject;
            }
            else
            {
                //if (selectedObject != null)
                //    selectedObject.GetComponent<GimmickBase>().GimmickUIsOnOff(false);
                selectedObject = null;
            }
        }
    }

    public Vector3 getCursorWorldPosition()
    {

        //Ray ray = Camera.main.ScreenPointToRay(cursor.transform.position);
        //RaycastHit hit;
        //Physics.Raycast(ray, out hit);
        //return new Vector3(hit.point.x,0,hit.point.y);
        Vector3 pos =Camera.main.ScreenToWorldPoint(new Vector3(cursor.transform.position.x, cursor.transform.position.y,Camera.main.transform.position.y));
        Debug.Log("cursor position = " + pos);
        return pos;
    }
    public Vector3 getCursorScreenPosition()
    {
        return cursor.transform.position;
    }
    /// <summary>
    /// 物を画面内に制限する
    /// </summary>
    /// <param name="tra"></param>
    public void DragRangeLimit(Transform tra)
    {
        var pos = tra.GetComponent<RectTransform>();
        float x = Mathf.Clamp(pos.position.x, 0, Screen.width*0.99f);
        float y = Mathf.Clamp(pos.position.y, Screen.height * 0.01f, Screen.height);
        pos.position = new Vector2(x, y);
    }

    private void PrepareGimmick(GameObject gimmick)
    {

        if (gimmick == null)
        {
            GimmickManager.Instance.ClearGimmick();
            return;
        }
        // UI展開
        //gimmick.GetComponent<GimmickBase>().GimmickUIsOnOff(true);
        // コントローラー入力待ち状態に送る
        if (gimmick.tag == "Gimmik")
        {
            gimmick.GetComponent<GimmickBase>().ClickGimmick(); 
        }
        
    }

    public void changeToSecondPhase()
    {
        SecondPhase = true;
        cursor.GetComponent<Image>().sprite = cursor_second;
    }

    public void addSingleOutline(Outline.Mode mode,Color color,float width)
    {
        if (width >= 10) width = 10;
        if (width <= 0) width = 0;
        var outline = outlineObject.AddComponent<Outline>();
        outline.OutlineMode = mode;
        outline.OutlineColor = color;
        outline.OutlineWidth = width;
    }
}
