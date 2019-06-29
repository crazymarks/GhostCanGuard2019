using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatueScript : MonoBehaviour
{

    public Grid grid;
    public Unit unit;

    public float smooth = 5.0f;
    public float tiltAngle = 60.0f;

    bool isFallen = false;

    float tiltAroundX;
    float tiltAroundZ;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow) && isFallen == false)
        {
            tiltAroundZ = tiltAngle;
            isFallen = true;
        }
        if (Input.GetKeyDown(KeyCode.RightArrow) && isFallen == false)
        {
            tiltAroundZ = -tiltAngle;
            isFallen = true;
            //grid.CreateGrid();
            //unit.RefindPath();
        }
        if (Input.GetKeyDown(KeyCode.UpArrow) && isFallen == false)
        {
            tiltAroundX = tiltAngle;
            isFallen = true;
        }
        if (Input.GetKeyDown(KeyCode.DownArrow) && isFallen == false)
        {
            tiltAroundX = -tiltAngle;
            isFallen = true;
        }

        if (Input.GetKeyDown(KeyCode.Space) && isFallen == true) resetPillar(); 
        

        Quaternion target = Quaternion.Euler(tiltAroundX, 0, tiltAroundZ);
        transform.rotation = Quaternion.Slerp(transform.rotation, target, Time.deltaTime * smooth);   
    }

    void resetPillar()
    {
        tiltAroundX = 0;
        tiltAroundZ = 0;
        isFallen = false;
    }

}
