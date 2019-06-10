using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ghost_targeting : MonoBehaviour
{
    public Transform targetpos;
    public float chasingSpeed = 5f;
    public float turnSpeed = 0.5f;
    public float walkSpeed = 2f;
    public bool isTargeting;
    public float distanceOfChasingPlayer;
    public float maxPatrolRange=5f;
    public float waitTime = 5f;

    Rigidbody rb;
    Vector3 missingPosition;
    Vector3 patroPosition;
    float distancetoPlayer;
    Quaternion targetQuaternion;
    
    float lastActTime;
    float patrolDistance = 0f;

    // Start is called before the first frame update
    void Start()
    {

        isTargeting = true;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (isTargeting)
            Chase();
        else
            patrol();
        
    }

    void Chase()
    {
        if (targetpos == null)
        {
            return;
        }
        transform.position = Vector3.MoveTowards(transform.position, targetpos.position, chasingSpeed * Time.deltaTime);
        if (targetpos.position - transform.position != Vector3.zero)
            targetQuaternion = Quaternion.LookRotation(targetpos.position - transform.position, Vector3.up);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetQuaternion, turnSpeed);
        lastActTime = Time.time;
        missingPosition = transform.position;
    }

    void patrol()
    {
        
        if (Time.time - lastActTime > waitTime)
        {
            generateRandomTarget();
            lastActTime = Time.time + (patroPosition-transform.position).magnitude / walkSpeed;
        }
        transform.position = Vector3.MoveTowards(transform.position, patroPosition, walkSpeed * Time.deltaTime);
        if (patroPosition - transform.position != Vector3.zero)
            targetQuaternion = Quaternion.LookRotation(patroPosition - transform.position, Vector3.up);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetQuaternion, turnSpeed);


    }

    void generateRandomTarget()
    {
        patrolDistance = -maxPatrolRange / (Random.Range(0f, maxPatrolRange - 1f) - maxPatrolRange);
        Debug.Log(patrolDistance);
        Vector2 point = Random.insideUnitCircle * patrolDistance;
        patroPosition = new Vector3(transform.position.x + point.x, transform.position.y, transform.position.z + point.y);
        if ((patroPosition - missingPosition).magnitude > maxPatrolRange)
        {
            Debug.Log("離れすぎから、もう一度生成します");
            generateRandomTarget();
        }
    }

}
