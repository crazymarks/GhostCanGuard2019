using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public Transform playerTrans;
    Vector3 playerPos;

    public float speed = 10.0f;

    void Start()
    {
        //playerTrans = this.gameObject.transform;
        //playerPos = playerTrans.position;
    }

    
    void Update()
    {
        playerPos = playerTrans.position;

        if (Input.GetKey(KeyCode.RightArrow))
        {
            playerTrans.position = new Vector3(playerPos.x + speed * Time.deltaTime, playerPos.y, playerPos.z);
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            playerTrans.position = new Vector3(playerPos.x - speed * Time.deltaTime, playerPos.y, playerPos.z);
        }
        if (Input.GetKey(KeyCode.UpArrow))
        {
            playerTrans.position = new Vector3(playerPos.x, playerPos.y, playerPos.z + speed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            playerTrans.position = new Vector3(playerPos.x, playerPos.y, playerPos.z - speed * Time.deltaTime);
        }
    }
}
