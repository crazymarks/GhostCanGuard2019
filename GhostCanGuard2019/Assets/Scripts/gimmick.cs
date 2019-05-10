using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gimmick : MonoBehaviour
{
    float now_time_scale = 1f;
    [SerializeField]
    private GameObject PauseUI;
    private GameObject ps;


    public void OnMouseDown()
    {

        if (Time.timeScale != 0)
        {
            now_time_scale = Time.timeScale;
            Time.timeScale = 0;
            ps = Instantiate(PauseUI)as GameObject;
            //ps = GetComponent<TextAsset>();
            Debug.Log("Pause");
        }
        else
        {
            Time.timeScale = now_time_scale;
            Destroy(ps);
            Debug.Log("ReStart");
        }
        

        
    }
    //public void 
}
