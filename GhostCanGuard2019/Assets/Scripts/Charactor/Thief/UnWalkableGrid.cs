using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnWalkableGrid
{
    public UnWalkableGrid(Vector3 position,float distance,LayerMask layer)
    {
        forward = Vector3.forward;
        forward.y = 0.1f;
        createVector3(position, distance,layer);
    }
    Vector3 forward;
    Vector3[] vector3s = new Vector3[4];

    void createVector3(Vector3 origin,float distance , LayerMask layer)
    {
       
        RaycastHit hit;
        for (int i = 0; i < vector3s.Length; i++)
        {
            Debug.DrawRay(origin, Quaternion.Euler(0, i * 90, 0) * forward.normalized, Color.green);
            Vector3 vector3;
            if (Physics.Raycast(origin, Quaternion.Euler(0, i * 90, 0) * forward, out hit, distance,layer,QueryTriggerInteraction.Ignore))
            {
                vector3 = hit.point - origin;
            }
            else
            {
                //vector3 =  Quaternion.Euler(0, i * 90, 0) * Vector3.forward * distance;
                vector3 = Vector3.zero;
            }
            vector3.y = 0.1f;
            vector3s[i] = vector3;
        }
        
    }

    public bool spherecheck(Vector3 gridcenter,Vector3 playerposition,float checkAngle)
    {
        Vector3 centery = gridcenter;
        Vector3 originy = playerposition;
        centery.y = 0.1f;
        originy.y = 0.1f;
        

        for (int i = 0; i < vector3s.Length; i++)
        {
            float angle = Mathf.Abs(Vector3.Angle(vector3s[i], centery - originy));
            float distance = Mathf.Abs(Vector3.Distance(centery, originy));
            if (angle <= checkAngle && distance <= Mathf.Abs(vector3s[i].magnitude))
            {
                return false;
            }
        }
     
        return true;
    }



}
