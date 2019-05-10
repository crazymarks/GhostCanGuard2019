using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Air_vent : gimmick
{
    Vector3 scale = new Vector3(3f, 3f, 3f);
    BoxCollider bc;
    // Start is called before the first frame update
    void Start()
    {
        bc = GetComponent<BoxCollider>();

        bc.size = scale;
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


  


}
