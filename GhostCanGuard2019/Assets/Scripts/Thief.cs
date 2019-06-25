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
        ESCAPE,
        PAUSE,
        STOP,
        END
    }
    public ThiefState thiefState = ThiefState.HEAD_TREASURE;

    public float headTreasureTimer = 5.0f; //duration of timer in escape before heading treasure
    public GameObject playerCollider;// collider object of the player , for passage blocking

    public bool isAlarmActivated = false; // for detecting alarm 

    Unit unit;// unit scrpt

    float escapeTimer = 0.0f;// timer in escape state
    float stayTimer = 0.0f; // timer in the radius of player, changes target after certain time
    float treasureTimer = 0.0f; // time taken to take treasure
    bool mIsPlayerExitedState = false, mIsTakenTreasure = false;// exited from radius 
    bool mIsPaused = false;


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
        if(mIsPlayerExitedState == true)
        {
            StartCounter();
        }
    }

    void HeadExitUpdate()
    {
        
    }

    void PauseUpdate()
    {

    }

    void StartCounter()
    {
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

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && thiefState != ThiefState.END)
        {
            Debug.Log("detected Player");
            mIsPlayerExitedState = false;
            thiefState = ThiefState.ESCAPE;
            playerCollider.SetActive(true);// activates the collider of player, thus not pasing that route
            unit.GetNewTarget();
            //unit.RefindPath();
            //unit.FollowPriority();
        }
        if(other.tag == "Treasure")
        {
            Debug.Log("inTreasure");
            thiefState = ThiefState.STOP;
        }
        if(other.tag == "Alarm")
        {
            Debug.Log("sensed alarm");
        }
        if(other.tag == "Exit" && thiefState == ThiefState.HEAD_EXIT)
        {
            Debug.Log("exited");
            thiefState = ThiefState.END;
        }
    }

    void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player" && thiefState != ThiefState.END)
        {
            stayTimer += Time.deltaTime;
            //if (stayTimer > 1.0f) unit.GetNewTarget();//changes escape target if still inside player radius for a certain time **hardcode 
        }
        if (other.tag == "Treasure")
        {
            treasureTimer += Time.deltaTime;
            if (treasureTimer > 1.0f)//time needed to collect treasure ** hardcode
            {
                mIsTakenTreasure = true;
                unit.HeadExit();
                thiefState = ThiefState.HEAD_EXIT;
            }
        }
        if(other.tag == "Alarm" && isAlarmActivated)// soon to be changed
        {
            unit.tempIncreaseSpeed();//increase thief speed during inside Alarm radius
        }
        if (other.tag == "Exit" && thiefState == ThiefState.HEAD_EXIT)
        {
            Debug.Log("exited");
            thiefState = ThiefState.END;
        }
    }
    void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            mIsPlayerExitedState = true;
        }
    }

}
