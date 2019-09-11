using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GimmikOutLine : MonoBehaviour
{

    Renderer Outline;
    private void OnEnable()
    {
        Outline = this.GetComponent<Renderer>();
    }
    void OnMouseOver()
    {
       
    }
}
