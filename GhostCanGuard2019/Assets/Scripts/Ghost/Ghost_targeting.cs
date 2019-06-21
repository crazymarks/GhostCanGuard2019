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
    Rigidbody trb;

    private Transform targetpos;
    
    Vector3 missingPosition;
    Vector3 patroPosition;
    float distancetoPlayer;
    Quaternion targetQuaternion;
    
    float lastActTime;
    float patrolDistance = 0f;

    

    // Start is called before the first frame update
    void Start()
    {
        
        if (thief == null)
        {
            thief = GameObject.FindGameObjectWithTag("Thief");
        }
        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player");
        }
        if (thief != null)
        {
            trb = thief.GetComponent<Rigidbody>();
        }
        
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
            
            move(chasingSpeed, trb.velocity.normalized);
        }
        if(targetpos == player.transform)
        {
            move(chasingSpeed, Vector3.zero);
        }
        
        lastActTime = Time.time;
    }

    void move(float speed, Vector3 advance_speed)
    {
        if((targetpos.position- transform.position).sqrMagnitude < 0.2f)  return;
        Vector3 moveSpeed = (targetpos.position + advance_speed - transform.position).normalized * speed * Time.deltaTime;
        transform.position += moveSpeed;
        //Debug.Log(moveSpeed);
        if (targetpos.position - transform.position != Vector3.zero)
            targetQuaternion = Quaternion.LookRotation(targetpos.position - transform.position, Vector3.up);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetQuaternion, turnSpeed);
    }


    void patrol()
    {
        if (Time.time - lastActTime > waitTime)
        {
            generateRandomTarget();
            lastActTime = Time.time + (patroPosition-transform.position).magnitude / walkSpeed;
        }
        
        move(walkSpeed, Vector3.zero);
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
        //Debug.Log(distancetoPlayer);
        if (distancetoPlayer < distanceOfChasingPlayer)
        {
            targetpos = player.transform;
        }
        if (distancetoPlayer > distanceOfGiveUpChase)
        {
            targetpos = thief.transform;
        }
        
    }
    public void bible(float time)
    {
       
        StartCoroutine(bibleEffect(time));
    }
    IEnumerator bibleEffect(float time)
    {
        chasingSpeed *= -1;
        yield return new WaitForSeconds(time);
        chasingSpeed *= -1;
    }
}
