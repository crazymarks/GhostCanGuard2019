using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    //** Values should be changed in the inspector**

    public Transform treasure, exit;// positions of treasure and exit
    public float initialSpeed = 20;// initial speed for thief
    public float increasedSpeed = 30, increasedSpeedDur = 3.0f;//increased speed and duration
    public float stunnedTime = 1.5f;// stunned gimick effect duration

    Vector3[] path;
    int targetIndex;
    float currSpeed;// current speed

    public List<Transform> escapePoints = new List<Transform>();
    public List<Transform> prirityPoints = new List<Transform>();

    Transform currTarget;// current target to head to

    Thief thief;

    private float nextActionTime = 0.0f;
    public float period = 0.1f;

    public bool mIsAllowFollow = false;
    bool mIsHeadable = false;

    void Start()
    {
        
        currTarget = treasure;
        currSpeed = initialSpeed;
        //InvokeRepeating("RefindPath", 0f, 0.5f);
        thief = GetComponent<Thief>();
        mIsAllowFollow = true;
        PathRequestManager.RequestPath(transform.position, currTarget.position, OnPathFound);

    }

    void Update()
    {
        //call this when
        //assuming path changed && after updating grid Gizmos
        //if (Input.GetKeyDown(KeyCode.A))// start pathfiding
        //{
        //    PathRequestManager.RequestPath(transform.position, currTarget.position, OnPathFound);
        //}
        if (Input.GetKeyDown(KeyCode.Q)) GotStunned();//stunned gimmick, 
        //PathRequestManager.RequestPath(transform.position, currTarget.position, OnPathFound);

        if (Time.time > nextActionTime && thief.thiefState != Thief.ThiefState.STOP)
        {
            nextActionTime += period;
            // execute block of code here
            //RefindPath();
        }


    }

    public void FollowPriority()
    {
        StartCoroutine(FindAndFollow());
    }

    public void tempIncreaseSpeed()
    {
        StartCoroutine(IncreaseSpeed(increasedSpeedDur));
    }

    public void GotStunned()
    {
        StartCoroutine(Stunned(stunnedTime));
    }

    public void GetNewTarget()
    {
        int rand = Random.Range(0, escapePoints.Capacity);
        currTarget = escapePoints[rand];
        PathRequestManager.RequestPath(transform.position, currTarget.position, OnPathFound);
    }

    public void HeadToTreasure()
    {
        currTarget = treasure;
        PathRequestManager.RequestPath(transform.position, currTarget.position, OnPathFound);
    }

    public void HeadExit()
    {
        currTarget = exit;
        PathRequestManager.RequestPath(transform.position, currTarget.position, OnPathFound);
    }

    public void OnPathFound(Vector3[] newPath, bool pathSuccessful)
    {
        if (pathSuccessful)
        {
            mIsAllowFollow = true;
            path = newPath;
            targetIndex = 0;
            StopCoroutine("FollowPath");
            StartCoroutine("FollowPath");
        }
        else
        {
            Debug.Log("what?");
            StopCoroutine("FollowPath");
            mIsAllowFollow = false;
        }
    }

    public void OnPathFound2(Vector3[] newPath, bool pathSuccessful)//  for priority
    {
        Debug.Log("hue");
        if (pathSuccessful)
        {
            Debug.Log("PathSuccess");
            mIsHeadable = true;
        }
    }

    public void RefindPath()
    {
        PathRequestManager.RequestPath(transform.position, currTarget.position, OnPathFound);
    }

    public void OnDrawGizmos()
    {
        if(path != null)
        {
            for(int i = targetIndex; i < path.Length; i++)
            {
                Gizmos.color = Color.black;
                Gizmos.DrawCube(path[i], Vector3.one);

                if(i == targetIndex)
                {
                    Gizmos.DrawLine(transform.position, path[i]);
                }
                else
                {
                    Gizmos.DrawLine(path[i - 1], path[i]);
                }
            }
        }
    }

    IEnumerator FindAndFollow()
    {
        //float temp = 0;
        int currIndex = 0;
        while (currIndex < prirityPoints.Count)
        {
            Debug.Log("called");
            PathRequestManager.RequestPath(transform.position, prirityPoints[currIndex].position, OnPathFound2);
            Debug.Log(mIsHeadable);
            //currSpeed = temp;
            //currSpeed = 0;
            //yield return new WaitForSeconds(0.2f);
            //currSpeed = temp;
            if (mIsHeadable)
            {
                Debug.Log("wee");
                PathRequestManager.RequestPath(transform.position, prirityPoints[currIndex].position, OnPathFound);
                yield break;
            }
            currIndex++;
        }

        if (!mIsHeadable)//All prioity targets not headable
        {
            //Do someting;
        }
    }

    IEnumerator FollowPath()
    {
        Vector3 currentWayPoint;
        if (path != null)
        {
            currentWayPoint = path[0];
            //Debug.Log(path[0]);
            //Debug.Log(path.Length);
        }
        else currentWayPoint = transform.position;

        while (true )
        {
            if(transform.position == currentWayPoint)
            {
                targetIndex++;
                if(targetIndex >= path.Length)
                {
                    yield break;
                }
                currentWayPoint = path[targetIndex];
            }
            transform.position = Vector3.MoveTowards(transform.position, currentWayPoint, currSpeed * Time.deltaTime);
            transform.LookAt(currentWayPoint);
            //transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(currentWayPoint, Vector3.up), 0.5f);
            yield return null;
        }
    }

    IEnumerator Stunned(float sec)
    {
        float temp = currSpeed;
        currSpeed = 0.0f;
        yield return new WaitForSeconds(sec);
        currSpeed = temp;
    }

    IEnumerator IncreaseSpeed(float sec)
    {
        float temp = currSpeed;
        currSpeed = increasedSpeed;
        yield return new WaitForSeconds(sec);
        currSpeed = initialSpeed;
    }
}
