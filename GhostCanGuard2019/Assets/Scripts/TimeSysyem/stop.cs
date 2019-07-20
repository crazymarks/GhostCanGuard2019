using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
//using System.Runtime.InteropServices;

public class stop : MonoBehaviour
{
    public LayerMask mask;
    [SerializeField]
    OutLineCamera outline;

    public GameObject selectedObject;
    [SerializeField]
    float speed = 10f;
    
    public GameObject cursor;

    public bool SecondPhase;

    bool stopped = false;
    //[DllImport("user32.dll")]
    //public static extern int SetCursorPos(float x, float y); //マウス位置を設定する

    //public void SetMouseToAnyOfScreenPosition()
    //{
    //    SetCursorPos(Camera.main.WorldToScreenPoint(cursor.transform.position).x, Camera.main.WorldToScreenPoint(cursor.transform.position).y);
    //}

    // Start is called before the first frame update
    void Start()
    {
        SecondPhase = false;
        outline = Camera.main.GetComponent<OutLineCamera>();
        outline.enabled = false;
        cursor.SetActive (false);
        //Cursor.visible = false;
        stopped = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Stop"))
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
            //SetMouseToAnyOfScreenPosition();
            if (!SecondPhase)
            {
                getObjectAtPosition();
            }
            
        }
            

    }
    public void gamestop()
    {
        if (GameManager.Instance.gameStart == false) return;
        if (stopped)
        {
            Time.timeScale = 1;
            cursor.SetActive(false);
            outline.enabled = false;
            PlayerAnimationController.Instance.SetAnimatorValue(SetPAnimator.Push);
            stopped=false;
        }
        else
        {
            cursor.SetActive( true);
            cursor.transform.position = Camera.main.WorldToScreenPoint(GameManager.Instance.pc.gameObject.transform.position);
            Time.timeScale = 0;
            outline.enabled = true;
            PlayerAnimationController.Instance.SetAnimatorValue(SetPAnimator.Hold);
            stopped = true;
        }
        
    }
    
    public void getObjectAtPosition()
    {
        Ray ray = Camera.main.ScreenPointToRay(cursor.transform.position);

        RaycastHit hit;
        if (Physics.Raycast(ray, out hit,mask) && hit.collider.gameObject != selectedObject)
        {
            if (hit.collider.tag == "Gimmik" || hit.collider.tag == "Player")
            {
                //EventSystem.current.SetSelectedGameObject(hit.collider.gameObject);
                //gimmickmanager.instance.GetGimick = hit.collidr.gamaobject;
                Debug.Log(hit.collider.gameObject.name);
                selectedObject = hit.collider.gameObject;
                
            }
            else
            {
                selectedObject = null;
            }
        }
    }

    public Vector3 getCursorWorldPosition()
    {
        Ray ray = Camera.main.ScreenPointToRay(cursor.transform.position);
        RaycastHit hit;
        Physics.Raycast(ray, out hit);
        return hit.point;
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
}
