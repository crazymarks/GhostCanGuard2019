using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Thief : MonoBehaviour
{
    public enum ThiefState
    {
        HEAD_TREASURE,
        IN_TREASURE,
        HEAD_EXIT,
        HEAD_GHOST,
        ESCAPE,
        PAUSE,
        STOP,
        EXITED,// player succeed
        END //player failed
    }
    public ThiefState thiefState = ThiefState.HEAD_TREASURE;

    public float headTreasureTimer = 5.0f; //duration of timer in escape before heading treasure
    public float idleTimer = 1.0f;
    public GameObject playerCollider;// collider object of the player , for passage blocking
    public GameObject ghostCollider;


    public bool isAlarmActivated = false; // for detecting alarm 

    Unit unit;// unit scrpt

    float escapeTimer = 0.0f;// timer in escape state
    float escapeTimer2 = 0.0f;
    float stayTimer = 0.0f; // timer in the radius of player, changes target after certain time
    float treasureTimer = 0.0f; // time taken to take treasure
    float idleTime = 0.0f;
    bool mIsPlayerExitedState = false, mIsTakenTreasure = false;// exited from radius 
    bool mIsPaused = false;
    bool mIsTouched = false;
    bool mIsAllowFInd = true;


    void Start()
    {

        unit = GetComponent<Unit>();
    }

    void Update()
    {
        if (thiefState == ThiefState.ESCAPE) EscapeUpdate();
        if (thiefState == ThiefState.PAUSE) PauseUpdate();
        if (thiefState == ThiefState.HEAD_EXIT) HeadExitUpdate();
    }

    void EscapeUpdate()
    {
        idleTime += Time.deltaTime;
        if (idleTime > idleTimer)
        {
            idleTime = 0f;
            mIsAllowFInd = true;
        }
        if (mIsPlayerExitedState == true)
        {
            StartCounter();
        }
        if (mIsTouched == true)
        {
            StartSecondaryCounter();
        }
    }

    void HeadExitUpdate()
    {
        StartSecondaryCounter();
        unit.HeadExit();
    }

    void PauseUpdate()
    {

    }

    void StartCounter()
    {
        //Debug.Log("startCOunter");
        escapeTimer += Time.deltaTime;
        if (escapeTimer > headTreasureTimer)
        {
            if (!mIsTakenTreasure)
            {
                unit.HeadToTreasure();
                escapeTimer = 0f;
                thiefState = ThiefState.HEAD_TREASURE;
            }
            else
            {
                unit.HeadExit();
                escapeTimer = 0f;
                thiefState = ThiefState.HEAD_EXIT;
            }
        }
    }
    void StartSecondaryCounter()
    {
        //Debug.Log("start2ndCOunter");
        escapeTimer2 += Time.deltaTime;
        if (escapeTimer2 > headTreasureTimer)
        {
            if (!mIsTakenTreasure)
            {
                unit.HeadToTreasure();
                escapeTimer2 = 0f;
                thiefState = ThiefState.HEAD_TREASURE;
            }
            else
            {
                unit.HeadExit();
                escapeTimer2 = 0f;
                thiefState = ThiefState.HEAD_EXIT;
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "PlayerCollider" && thiefState != ThiefState.END && mIsAllowFInd == true)
        {
            mIsAllowFInd = false;
            Debug.Log("detected Player");
            mIsPlayerExitedState = false;
            thiefState = ThiefState.ESCAPE;
            playerCollider.SetActive(true);// activates the collider of player, thus not pasing that route
            unit.GetNewTarget();
            mIsTouched = true;
            //unit.RefindPath();
            //unit.FollowPriority();
            //StartCounter();
        }
        else if (other.tag == "Ghost" && thiefState != ThiefState.END && mIsAllowFInd == true)
        {
            mIsAllowFInd = false;
            Debug.Log("detected Ghost");
            mIsPlayerExitedState = false;
            thiefState = ThiefState.ESCAPE;
            if(ghostCollider!=null)ghostCollider.SetActive(true);// activates the collider of player, thus not pasing that route
            unit.GetNewTarget();
            //unit.RefindPath();
            //unit.FollowPriority();
        }
        else if (other.tag == "Treasure")
        {
            Debug.Log("inTreasure");
            thiefState = ThiefState.STOP;
        }
        else if (other.tag == "Alarm")
        {
            Debug.Log("sensed alarm");
        }
        else if (other.tag == "Exit" && thiefState == ThiefState.HEAD_EXIT)
        {
            Debug.Log("exited");
            thiefState = ThiefState.END;
        }
        else if (other.tag == "Exit" && !mIsTakenTreasure)
        {
            thiefState = ThiefState.EXITED;
        }
    }

    void OnTriggerStay(Collider other)
    {

        if ((other.tag == "Player" || other.tag == "Ghost") && thiefState != ThiefState.END)
        {
            mIsPlayerExitedState = false;
            stayTimer += Time.deltaTime;
            //if (stayTimer > 2.0f) unit.GetNewTarget();//changes escape target if still inside player radius for a certain time **hardcode 
        }
        else if (other.tag == "Treasure")
        {
            treasureTimer += Time.deltaTime;
            if (treasureTimer > 1.0f)//time needed to collect treasure ** hardcode
            {
                mIsTakenTreasure = true;
                unit.HeadExit();
                thiefState = ThiefState.HEAD_EXIT;
            }
        }
        else if (other.tag == "Alarm" && isAlarmActivated)// soon to be changed
        {
            unit.tempIncreaseSpeed();//increase thief speed during inside Alarm radius
        }
        else if (other.tag == "Exit" && thiefState == ThiefState.HEAD_EXIT)
        {
            Debug.Log("exited");
            thiefState = ThiefState.END;
        }
    }
    void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player" || other.tag == "Ghost")
        {
            Debug.Log("exitedtrigger");
            mIsPlayerExitedState = true;
            mIsTouched = false;
        }
    }

}
