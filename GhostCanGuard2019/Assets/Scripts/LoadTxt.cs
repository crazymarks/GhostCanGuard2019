using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class LoadTxt : MonoBehaviour
{
    
    // Start is called before the first frame update
    public void OnMouseDown()
    {
        
        var texta = Resources.Load("aaa.txt") as TextAsset;
        string txt = (Resources.Load("aaa.txt") as TextAsset).text;

    }

    public void LoadasTextAsset()
    {
        
    }
    


}
