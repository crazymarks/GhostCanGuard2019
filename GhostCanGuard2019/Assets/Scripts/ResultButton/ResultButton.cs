using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResultButton : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Transform myTransform = this.transform;

        Vector3 worldpos = myTransform.position;
        worldpos.x = 800.0f;
        worldpos.y = 350.0f;
        worldpos.z = 0.0f;
        myTransform.position = worldpos;
    }
}
