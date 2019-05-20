using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseHit : MonoBehaviour
{
    
    Material material;
    // Start is called before the first frame update
    void Start()
    {
        material = GetComponent<Renderer>().material;
    }

  
   
    private void OnMouseOver()
    {
        
        material.SetFloat("_OutlineWidth", 1.3f);
        Debug.Log(name);
    }
    private void OnMouseExit()
    {
        material.SetFloat("_OutlineWidth", 1f);
    }

}
