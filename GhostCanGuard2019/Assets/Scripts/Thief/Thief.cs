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

    public float headTreasureTimer = 0.5f; //duration of timer in escape before heading treasure
    public float idleTime = 1.0f;
    public SphereCollider playerCollider;// collider object of the player , for passage blocking
    public SphereCollider ghostCollider;
    float colliderradius;

    public bool isAlarmActivated = false; // for detecting alarm 

    Unit unit;// unit scrpt

    float escapeTimer = 0.0f;// timer in escape state
    float escapeTimer2 = 0.0f;
    float stayTimer = 0.0f; // timer in the radius of player, changes target after certain time
    float treasureTimer = 0.0f; // time taken to take treasure
    float idleTimer = 0.0f;
    bool mIsPlayerExitedState = false;
    public bool mIsTakenTreasure { get; private set; }// exited from radius 
    bool mIsPaused = false;
    bool mIsTouched = false;
    bool mIsAllowFind = true;

    /// <summary>
    /// animation
    /// </summary>
    [SerializeField]
    ThiefAnimationController anim = null;

    void Start()
    {
        unit = GetComponent<Unit>();
        anim.setRunAnimation();
        mIsTakenTreasure = false;
        colliderradius = ghostCollider.radius;
    }

    void Update()
    {
        switch (thiefState)
        {
            case ThiefState.HEAD_TREASURE:
                break;
            case ThiefState.IN_TREASURE:
                InTreasureUpdate();
                break;
            case ThiefState.HEAD_EXIT:
                HeadExitUpdate();
                break;
            case ThiefState.HEAD_GHOST:
                break;
            case ThiefState.ESCAPE:
                EscapeUpdate();
                break;
            case ThiefState.PAUSE:
                PauseUpdate();
                break;
            case ThiefState.STOP:
                break;
            case ThiefState.EXITED:
                break;
            case ThiefState.END:
                break;
            default:
                break;
        }
        
    }

    void EscapeUpdate()
    {
        idleTimer += Time.deltaTime;
        if (idleTimer > idleTime)
        {
            idleTimer = 0f;
            mIsAllowFind = true;
        }
        if (mIsPlayerExitedState == true)
        {
            StartCounter();
        }
        //if (mIsTouched == true)
        //{
        //    StartSecondaryCounter(); 
        //}
    }

    void HeadExitUpdate()
    {
        StartCounter();
        //StartSecondaryCounter();
        //unit.HeadExit();  　　　　　　　　　　　　　　　　　
    }

    void PauseUpdate()
    {

    }

    void InTreasureUpdate()
    {
        treasureTimer += Time.deltaTime;
        if (treasureTimer > 1.0f)//time needed to collect treasure ** hardcode
        {
            mIsTakenTreasure = true;
            unit.HeadToExit();
            thiefState = ThiefState.HEAD_EXIT;
            anim.setRunAnimation();
            treasureTimer = 0f;
        }
    }


    void StartCounter()
    {
        //Debug.Log("startCounter");
        escapeTimer += Time.deltaTime;
        if (escapeTimer > headTreasureTimer)
        {
            if (!mIsTakenTreasure)
            {
                unit.HeadToTreasure();
                escapeTimer = 0f;
                thiefState = ThiefState.HEAD_TREASURE;
                anim.setRunAnimation();
            }
            else
            {
                unit.HeadToExit();
                escapeTimer = 0f;
                thiefState = ThiefState.HEAD_EXIT;
                anim.setRunAnimation();
            }
        }
    }
    //void StartSecondaryCounter() //
    //{
    //    //Debug.Log("start2ndCounter");
    //    escapeTimer2 += Time.deltaTime;
    //    if (escapeTimer2 > headTreasureTimer)
    //    {
    //        if (!mIsTakenTreasure)
    //        {
    //            unit.HeadToTreasure();
    //            escapeTimer2 = 0f;
    //            thiefState = ThiefState.HEAD_TREASURE;
    //            anim.setRunAnimation();
    //        }
    //        else
    //        {
    //            unit.HeadToExit();
    //            escapeTimer2 = 0f;
    //            thiefState = ThiefState.HEAD_EXIT;
    //            anim.setRunAnimation();
    //        }
    //    }
    //}

    void OnTriggerEnter(Collider other)
    {
        
        if (other.tag == "PlayerCollider" && thiefState != ThiefState.END && mIsAllowFind == true)
        {
            mIsAllowFind = false;
            Debug.Log("detected Player");
            mIsPlayerExitedState = false;
            thiefState = ThiefState.ESCAPE;
            //playerCollider.SetActive(true);// activates the collider of player, thus not pasing that route
            unit.HeadToEscapePoint();
            //mIsTouched = true;
            //unit.RefindPath();
            //unit.FollowPriority();
            //StartCounter();
        }
        else if (other.tag == "Ghost" && thiefState != ThiefState.END && mIsAllowFind == true)
        {
            mIsAllowFind = false;
            Debug.Log("detected Ghost");
            mIsPlayerExitedState = false;
            thiefState = ThiefState.ESCAPE;
            //if(ghostCollider!=null)ghostCollider.SetActive(true);// activates the collider of player, thus not pasing that route
            unit.HeadToEscapePoint();
            //unit.RefindPath();
            //unit.FollowPriority();
        }
        //else if (other.tag == "Treasure")
        //{
        //    Debug.Log("inTreasure");
        //    thiefState = ThiefState.IN_TREASURE;
        //    anim.setWaitAnimation();
        //}
        else if (other.tag == "Alarm")
        {
            Debug.Log("sensed alarm");
        }
        //else if (other.tag == "Exit" && thiefState == ThiefState.HEAD_EXIT)
        //{
        //    Debug.Log("exited");
        //    thiefState = ThiefState.END;
        //    anim.setWaitAnimation();
        //}
        else if (other.tag == "Exit" && !mIsTakenTreasure)
        {
            thiefState = ThiefState.EXITED;
        }
    }

    void OnTriggerStay(Collider other)
    {

        if ((other.tag == "PlayerCollider" || other.tag == "Ghost") && thiefState != ThiefState.END)
        {
            mIsPlayerExitedState = false;
            stayTimer += Time.deltaTime;
            if (stayTimer > 0.1f)
            {   unit.HeadToEscapePoint();//changes escape target if still inside player radius for a certain time **hardcode
                stayTimer = 0f;
            }
        }
        //else if (other.tag == "Treasure")
        //{
        //    //if (!mIsTakenTreasure)
        //    //{
        //    //    thiefState = ThiefState.IN_TREASURE;
        //    //    anim.setWaitAnimation();
        //    //}
        //    if (thiefState != ThiefState.IN_TREASURE)
        //    {
        //        treasureTimer += Time.deltaTime;
        //        if (treasureTimer > 1.0f)//time needed to collect treasure ** hardcode
        //        {
        //            mIsTakenTreasure = true;
        //            unit.HeadToExit();
        //            thiefState = ThiefState.HEAD_EXIT;
        //            anim.setRunAnimation();
        //        }
        //    }
            
        //}
        else if (other.tag == "Alarm" && isAlarmActivated)// soon to be changed
        {
            unit.tempIncreaseSpeed();//increase thief speed during inside Alarm radius
        }
        //else if (other.tag == "Exit" && thiefState == ThiefState.HEAD_EXIT)
        //{
        //    Debug.Log("exit_success");
        //    thiefState = ThiefState.END;
        //    anim.setWaitAnimation();
        //}
    }
    void OnTriggerExit(Collider other)
    {
        if (other.tag == "PlayerCollider" || other.tag == "Ghost")
        {
            Debug.Log("exitedtrigger");
            mIsPlayerExitedState = true;
            mIsTouched = false;
        }
    }

    /// <summary>
    /// 終点チェック
    /// </summary>
    public void reachEscapePoint()
    {
       
        //mIsAllowFind = false;
        //mIsPlayerExitedState = false;
        Debug.Log("ReachTarget");
        if (mIsTakenTreasure)
        {
            if (unit.currTarget == unit.exit && thiefState == ThiefState.HEAD_EXIT && GameManager.Instance.getXZDistance(unit.exit.gameObject,unit.gameObject) <= 2f)
            {
                Debug.Log("exit_success");
                thiefState = ThiefState.END;
                anim.setWaitAnimation();
            }
           
            else
            {
                thiefState = ThiefState.HEAD_EXIT;
                unit.HeadToExit();
            }
        }
        else
        {
            if (unit.currTarget == unit.treasure && GameManager.Instance.getXZDistance(unit.treasure.gameObject, unit.gameObject) <= 2f)
            {
                if (thiefState != ThiefState.IN_TREASURE)
                {
                    Debug.Log("inTreasure");
                    thiefState = ThiefState.IN_TREASURE;
                    anim.setWaitAnimation();
                }
            }
            else
            {
                thiefState = ThiefState.HEAD_TREASURE;
                unit.HeadToTreasure();
            }
        }
        setGhostCollider();
        //if(ghostCollider!=null)ghostCollider.SetActive(true);// activates the collider of player, thus not pasing that route
       
    }

    public void setGhostCollider()
    {
        if (ghostCollider != null) ghostCollider.radius = colliderradius;
    }

}
