using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

    bool ghostEnable = false;
    [SerializeField]
    private Ghost_targeting ght;
    [SerializeField]
    private LoadScene ldc;
    bool gameover = false;
    [SerializeField]
    float checkdistance = 0.2f;
    float distance_player_to_thief;
    float distance_player_to_ghost;
    float distance_ghost_to_thief;



    bool gameStart = false;
    [SerializeField]
    float startWait = 2f;
    [SerializeField]
    Text text;



    [SerializeField]
    private GameObject treasure;
    private ParticleSystem swordlight;

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

        if (treasure == null)
        {
            treasure = GameObject.FindGameObjectWithTag("Treasure");
        }
        if (treasure != null)
        {
            swordlight = treasure.GetComponentInChildren<ParticleSystem>(true);
        }

        gameStart = false;
        text.text = "";
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
                text.text = "泥棒が逃げました！";
                text.enabled = true;
                Debug.Log("泥棒が逃げました！");
                gameover = true;
            }
            if (tf.thiefState == Thief.ThiefState.EXITED)
            {
                text.text = "平和な夜ですね．．．";
                text.enabled = true;
                Debug.Log("You Win!");
                gameover = true;
            }
            if (distance_player_to_thief <= checkdistance)
            {
                text.text = "逮捕!";
                text.enabled = true;
                Debug.Log("You Win!");
                gameover = true;
            }
            if (ghostEnable)
            {
                if (distance_ghost_to_thief <= checkdistance)
                {
                    text.text = "迷えば、敗れる";
                    text.enabled = true;
                    Debug.Log("泥棒が殺人鬼に殺された！！");
                    gameover = true;
                }
                if (distance_player_to_ghost <= checkdistance)
                {
                    text.text = "死";
                    text.enabled = true;
                    Debug.Log("死");
                    gameover = true;
                }
            }
            if (tf.thiefState == Thief.ThiefState.HEAD_EXIT && swordlight != null)
            {
                swordlight.Stop();
                swordlight.Clear();
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
        for (int i = (int)startTime + 1; i >= 0; i--)
        {
            if (i > 0)
            {
                text.text = (i.ToString());
            }
            else
                text.text = "Start!!";
            Debug.Log(i + 1 + "..");
            yield return new WaitForSecondsRealtime(1f);
        }
        text.enabled = false;
        Debug.Log("start!!");
        gameStart = true;
        Time.timeScale = 1f;
        GimmickManager.Instance.GimmickFrag = true;
    }
    public float getXZDistance(GameObject a, GameObject b)
    {
        float distance = Mathf.Sqrt((a.transform.position.x - b.transform.position.x) * (a.transform.position.x - b.transform.position.x) + (a.transform.position.z - b.transform.position.z) * (a.transform.position.z - b.transform.position.z));
        return distance;
    }
    public IEnumerator showTextWithSeconds(string msg,float seconds)
    {
        Time.timeScale = 0;
        text.text = msg;
        text.enabled = true;
        yield return new WaitForSecondsRealtime(seconds);
        text.enabled = false;
        Time.timeScale = 1;
    }
}
