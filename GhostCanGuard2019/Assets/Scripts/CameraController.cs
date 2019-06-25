using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform playerPos;
    public float offset = 100.0f;

    void Start()
    {
        transform.position = new Vector3(playerPos.position.x, playerPos.position.y + offset, playerPos.position.z);
    }

    // Update is called once per frame
    void LateUpdate()
    {
        transform.position = new Vector3(playerPos.position.x, playerPos.position.y + offset, playerPos.position.z);
    }
}
