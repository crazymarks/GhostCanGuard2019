using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : SingletonMonoBehavior<GameManager>
{
    //[SerializeField]
    //private PlayerManager pmanager;
    //[SerializeField]
    //private GimmickManager gmkmanager;
    [SerializeField]
    private Thief tf;
    [SerializeField]
    private PlayerControl pc;

    bool ghostEnable=false;
    [SerializeField]
    private Ghost_targeting ght;
    [SerializeField]
    private LoadScene ldc;
    bool gameover=false;
    [SerializeField]
    float checkdistance = 0.2f;
    float distance_player_to_thief;
    float distance_player_to_ghost;
    float distance_ghost_to_thief;

    bool gameStart=false;
    [SerializeField]
    float startWait=2f;

    // Start is called before the first frame update
    void Start()
    {
        if (pc == null)
        {
            pc = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerControl>();
        }
        if (tf == null)
        {
            tf = GameObject.FindGameObjectWithTag("Thief").GetComponent<Thief>();
        }
        if (ght == null)
        {
            try
            {
                ght = GameObject.FindGameObjectWithTag("Ghost").GetComponent<Ghost_targeting>();
            }
            catch (System.Exception)
            {
                if (ght == null)
                {
                    Debug.Log("Ghost Dosen't Exist");
                }
                else
                    throw;
            }
            
        }
        if (ght != null)
        {
            ghostEnable = true;
        }
        gameStart = false;
        GimmickManager.Instance.GimmickFrag = false;
        StartCoroutine(startCount(startWait));
    }

    // Update is called once per frame
    void Update()
    {
     
        if (ghostEnable)
        {
            distance_ghost_to_thief = getXZDistance(ght.gameObject, tf.gameObject);
            distance_player_to_ghost = getXZDistance(pc.gameObject, ght.gameObject);
        }
        
        distance_player_to_thief = getXZDistance(pc.gameObject, tf.gameObject);
        if (!gameover)
        {
            //Debug.Log(distance_player_to_thief);
            if (tf.thiefState == Thief.ThiefState.END)
            {
                Debug.Log("泥棒が逃げました！");
                gameover = true;
            }
            
            if (distance_player_to_thief <= checkdistance)
            {
                Debug.Log("You Win!");
                gameover = true;
            }
            if (ghostEnable)
            {
                if (distance_ghost_to_thief <= checkdistance)
                {
                    Debug.Log("泥棒が殺人鬼に殺された！！");
                    gameover = true;
                }
                if (distance_player_to_ghost <= checkdistance)
                {
                    Debug.Log("死");
                    gameover = true;
                }
            }
            
        }
        else
        {
            StartCoroutine(ie());
        }
    }

    IEnumerator ie()
    {
        Time.timeScale = 0;
        yield return new WaitForSecondsRealtime(2f);
        Time.timeScale = 1f;
        ldc.loadScene("TitleScene");

    }
    IEnumerator startCount(float startTime)
    {
        Time.timeScale = 0;
        for(int i = (int)startTime; i > 0; i--)
        {
            Debug.Log(i+"..");
            yield return new WaitForSecondsRealtime(1f);
        }
        Debug.Log("start!!");
        gameStart = true;
        Time.timeScale = 1f;
        GimmickManager.Instance.GimmickFrag = true;
    }
    float getXZDistance(GameObject a, GameObject b)
    {
        float distance = Mathf.Sqrt((a.transform.position.x - b.transform.position.x) * (a.transform.position.x - b.transform.position.x) + (a.transform.position.z - b.transform.position.z) * (a.transform.position.z - b.transform.position.z));
        return distance;
    }
}
