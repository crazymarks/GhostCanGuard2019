using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
//using System.Runtime.InteropServices;

public class stop : MonoBehaviour
{
    
    public GameObject selectedObject;
    [SerializeField]
    float speed = 10f;
    [SerializeField]
    public GameObject cursor;

    //[DllImport("user32.dll")]
    //public static extern int SetCursorPos(float x, float y); //マウス位置を設定する

    //public void SetMouseToAnyOfScreenPosition()
    //{
    //    SetCursorPos(Camera.main.WorldToScreenPoint(cursor.transform.position).x, Camera.main.WorldToScreenPoint(cursor.transform.position).y);
    //}

    // Start is called before the first frame update
    void Start()
    {
        cursor.SetActive (false);
        //Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Stop") || Input.GetKeyDown(KeyCode.Space))
        {
            gamestop();
        }
        if (Time.timeScale == 0)
        {
            float horizontal = Input.GetAxisRaw("Horizontal");
            float vertical = Input.GetAxisRaw("Vertical");
            Vector2 move = new Vector2(horizontal, vertical);

            cursor.transform.Translate(move * speed);
            DragRangeLimit(cursor.transform);
            //SetMouseToAnyOfScreenPosition();

            getPosition();
        }
            

    }
    public void gamestop()
    {
        if (GameManager.Instance.gameStart == false) return;
        if (Time.timeScale == 0)
        {
            Time.timeScale = 1;
            cursor.SetActive( false);
        }
        else
        {
            cursor.SetActive( true);
            cursor.transform.position = Camera.main.WorldToScreenPoint(GameManager.Instance.pc.gameObject.transform.position);
            Time.timeScale = 0;

            GimmickManager.Instance.GimmicksOpenUI();
        }
        
    }
    
    public void getPosition()
    {
        Ray ray = Camera.main.ScreenPointToRay(cursor.transform.position);

        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
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

    /// <summary>
    /// 物を画面内に制限する
    /// </summary>
    /// <param name="tra"></param>
    public void DragRangeLimit(Transform tra)
    {
        var pos = tra.GetComponent<RectTransform>();
        float x = Mathf.Clamp(pos.position.x, pos.rect.width * 0.5f, Screen.width - (pos.rect.width * 0.5f));
        float y = Mathf.Clamp(pos.position.y, pos.rect.height * 0.5f, Screen.height - (pos.rect.height * 0.5f));
        pos.position = new Vector2(x, y);
    }
}
