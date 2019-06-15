using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ghost_targeting : MonoBehaviour
{
    
    public float chasingSpeed = 5f;
    public float turnSpeed = 0.5f;
    public float walkSpeed = 2f;
    public bool isTargeting;
    public float distanceOfChasingPlayer = 10f;
    public float distanceOfGiveUpChase = 15f;
    public float maxPatrolRange=5f;
    public float waitTime = 5f;

    GameObject thief;
    GameObject player;


    private Transform targetpos;
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
        rb = GetComponent<Rigidbody>();
        thief = GameObject.FindGameObjectWithTag("Thief");
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (isTargeting)
            Chase();
        else
            patrol();
    }
    private void Update()
    {
        targetCheck();
    }
    void Chase()
    {
        if (targetpos == null)
        {
            missingPosition = transform.position;
            return;
        }
        if (targetpos == thief.transform)
        {
            Rigidbody trb = thief.GetComponent<Rigidbody>();
            rb.position =Vector3.MoveTowards(transform.position, targetpos.position + trb.velocity.normalized, chasingSpeed * Time.deltaTime);
            if (targetpos.position - transform.position != Vector3.zero)
                targetQuaternion = Quaternion.LookRotation(targetpos.position - transform.position, Vector3.up);
            rb.rotation = Quaternion.Slerp(transform.rotation, targetQuaternion, turnSpeed);
        }
        if(targetpos == player.transform)
        {
            rb.position = Vector3.MoveTowards(transform.position, targetpos.position, chasingSpeed * Time.deltaTime);
            if (targetpos.position - transform.position != Vector3.zero)
                targetQuaternion = Quaternion.LookRotation(targetpos.position - transform.position, Vector3.up);
            rb.rotation = Quaternion.Slerp(transform.rotation, targetQuaternion, turnSpeed);
        }
        
        lastActTime = Time.time;
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

    void targetCheck()
    {
        if (thief == null || player == null)
            return;
        if (targetpos == null)
        {
            targetpos = thief.transform;
        }
        distancetoPlayer = Vector3.Distance(transform.position, player.transform.position);
        Debug.Log(distancetoPlayer);
        if (distancetoPlayer < distanceOfChasingPlayer)
        {
            targetpos = player.transform;
        }
        if (distancetoPlayer > distanceOfGiveUpChase)
        {
            targetpos = thief.transform;
        }
        
    }

}
