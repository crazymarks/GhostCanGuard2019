using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class GameManager : SingletonMonoBehavior<GameManager>
{
    //[SerializeField]
    //private PlayerManager pmanager;
    //[SerializeField]
    //private GimmickManager gmkmanager;
  
    [SerializeField]
    private Thief tf;  //泥棒を取得
    [SerializeField]
    public PlayerControl pc;  //Playerを取得

    bool ghostEnable = true;  //殺人鬼があるかどうか
    [SerializeField]
    private Ghost_targeting ght;  //殺人鬼を取得

    [SerializeField]
    private LoadScene ldc;  //Scene管理コンポーネント

    bool gameover = false;   //ゲーム状態flag
    [SerializeField]
    float checkdistance = 0.2f;     //ゲーム勝負の判定距離

    float distance_player_to_thief;
    float distance_player_to_ghost;
    float distance_ghost_to_thief;



    public bool gameStart { get; private set; }  //ゲームが始まるかどうかのflag
    [SerializeField]
    float startWait = 2f;   //始まるまでの時間設定

    [SerializeField]
    Text text;      //ゲームメッセージを表すメッセージボックス



    [SerializeField]
    private GameObject treasure;
    private ParticleSystem swordlight;

    // Start is called before the first frame update
    void Start()
    {
        
        ///キャラクタをそれぞれ取得
        if (pc == null)
        {
            try
            {
                pc = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerControl>();
            }
            catch (System.NullReferenceException)
            {
                Debug.Log("プレイヤー未発見" + name);
            }
            
        }
        if (tf == null)
        {
            try
            {
                tf = GameObject.FindGameObjectWithTag("Thief").GetComponent<Thief>();
            }
            catch (System.NullReferenceException)
            {
                Debug.Log("Thief未発見" + name);
            }
           
        }
        if (ght == null)
        {
            try
            {
                ght = GameObject.FindGameObjectWithTag("Ghost").GetComponent<Ghost_targeting>();
            }
            catch (System.NullReferenceException)
            {
                Debug.Log("Ghost Dosen't Exist");
                ghostEnable = false;
            }

        }
        

        if (treasure == null)
        {
            try
            {
                treasure = GameObject.FindGameObjectWithTag("Treasure");
            }
            catch (System.NullReferenceException)
            {
                Debug.Log("宝未指定" + name);
            }
        }
        if (treasure != null)
        {
            try
            {
                swordlight = treasure.GetComponentInChildren<ParticleSystem>(true);
            }
            catch (System.NullReferenceException)
            {

                Debug.Log("");
            }
            
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
                UIManager.Instance.ShowDesPanel("泥棒が逃げました！");
                //text.text = "泥棒が逃げました！";
                //text.enabled = true;
                Debug.Log("泥棒が逃げました！");
                gameover = true;
            }
            if (tf.thiefState == Thief.ThiefState.EXITED)
            {
                UIManager.Instance.ShowDesPanel("平和な夜ですね．．．");
                //text.text = "平和な夜ですね．．．";
                //text.enabled = true;
                Debug.Log("You Win!");
                gameover = true;
            }
            if (distance_player_to_thief <= checkdistance)
            {
                UIManager.Instance.ShowDesPanel("逮捕成功!");
                //text.text = "逮捕成功!";
                //text.enabled = true;
                Debug.Log("You Win!");
                gameover = true;
            }
            if (ghostEnable)
            {
                if (distance_ghost_to_thief <= checkdistance)
                {
                    UIManager.Instance.ShowDesPanel("迷えば、敗れる");
                    //text.text = "迷えば、敗れる";
                    //text.enabled = true;
                    Debug.Log("泥棒が殺人鬼に殺された！！");
                    gameover = true;
                }
                if (distance_player_to_ghost <= checkdistance)
                {
                    UIManager.Instance.ShowDesPanel("死");
                    //text.text = "死";
                    //text.enabled = true;
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

    private void theWorld()
    {
        Time.timeScale = 0.1f;
    }




}
