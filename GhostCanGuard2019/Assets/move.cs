using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class move : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKey(KeyCode.UpArrow)) transform.Translate(Vector3.forward * Time.deltaTime);
    }
}
