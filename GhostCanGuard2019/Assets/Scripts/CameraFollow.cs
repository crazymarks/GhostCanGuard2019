using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    public float smothing = 5f;


    Vector3 offset;
    // Start is called before the first frame update
    void Start()
    {
        transform.position = target.position + new Vector3(0f, 10f, 0f);
        offset = transform.position - target.position;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 targetcampos = target.position + offset;
        transform.position = Vector3.Lerp(transform.position, targetcampos, smothing * Time.deltaTime);

    }
}
