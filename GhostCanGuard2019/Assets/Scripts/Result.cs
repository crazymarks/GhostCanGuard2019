using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Result : MonoBehaviour
{
    
    // Start is called before the first frame update
    void Start()
    {
        transform.Rotate(new Vector3(0,0,90) );
        
        
    }

    // Update is called once per frame
    void Update()
    {
        Transform myTransform = this.transform;

        Vector3 worldpos = myTransform.position;
        worldpos.x = 240.0f;
        worldpos.y = 200.0f;
        worldpos.z = 0.0f;
        myTransform.position = worldpos;
        
    }
}
