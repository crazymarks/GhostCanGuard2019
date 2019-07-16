using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class stop : MonoBehaviour
{

    [SerializeField]
    float speed = 10f;
    [SerializeField]
    Image cursor;
    // Start is called before the first frame update
    void Start()
    {
        cursor.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        gamestop();

        if (Time.timeScale == 0)
        {
            float horizontal = Input.GetAxisRaw("Horizontal");
            float vertical = Input.GetAxisRaw("Vertical");
            Vector2 move = new Vector2(horizontal, vertical);
            cursor.rectTransform.Translate(move * speed);
            getPosition();
        }
            

    }
    void gamestop()
    {
        if (Input.GetButtonDown("Stop"))
        {
            if (Time.timeScale == 0)
            {
                Time.timeScale = 1;
                cursor.enabled = false;
            }
            else
            {
                cursor.enabled = true;

                Time.timeScale = 0;

            }
        }
    }
    void getPosition()
    {
        Ray ray = Camera.main.ScreenPointToRay(cursor.transform.position);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            if (hit.collider.tag == "gimmick" || hit.collider.tag == "player")
            {
                
                //gimmickmanager.instance.GetGimick = hit.collidr.gamaobject;
                Debug.Log(hit.collider.gameObject.name);
            }
            //else
            //{
            //    gimmickmanager.instance.GetGimick = null;
            //    return;
            //}
        }

    }
}
