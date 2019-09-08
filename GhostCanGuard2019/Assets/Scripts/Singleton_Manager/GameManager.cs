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
    private Thief tf;  //泥棒を取得
    [SerializeField]
    public PlayerControl pc { get; private set; }  //Playerを取得

    bool ghostEnable = true;  //殺人鬼があるかどうか
    [SerializeField]
    private Ghost_targeting ght;  //殺人鬼を取得

    

    public bool gameover { get; private set; } = false;   //ゲーム状態flag
    
    [Header("勝負の判定距離")]
    public float checkdistance = 0.2f;     //ゲーム勝負の判定距離

    float distance_player_to_thief;
    float distance_player_to_ghost;
    float distance_ghost_to_thief;

   

    /// <summary>
    /// gameStart
    /// </summary>
    public bool IfGameStart { get; private set; }  //ゲームが始まるかどうかのflag
    public GameObject StartCount;
    //[SerializeField]
    //float startWait = 2f;   //始まるまでの時間設定

    [SerializeField]
    Text text = null;      //ゲームメッセージを表すメッセージボックス

    /// <summary>
    /// GameOver
    /// </summary>
    public GameObject arrestCanvas;
    public GameObject deadCanvas;
    public GameObject policeDeadCanvas;
    public GameObject stealCanvas;
    GameObject endingscene;

    enum GameOverState
    {
        arrest,
        thiefDead,
        policeDead,
        steal
    }
    GameOverState OverState;

    [SerializeField]
    private GameObject treasure;
    //private ParticleSystem swordlight;
    //GameObject sword;

    StopSystem st;

    bool iestart = false;  /////後で消す必要

    // Start is called before the first frame update
    void Start()
    {
        iestart = false;
       
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
        //if (treasure != null)
        //{
        //    try
        //    {
        //        swordlight = treasure.GetComponentInChildren<ParticleSystem>(true);
        //    }
        //    catch (System.NullReferenceException)
        //    {

        //        Debug.Log("");
        //    }
            
        //}
        if (ght)
        {
            ghostEnable = ght.gameObject.activeSelf;
        }
        st = GetComponent<StopSystem>();
        IfGameStart = false;
        text.text = "";
        GimmickManager.Instance.GimmickFrag = false;
        playStartCount();
        //StartCoroutine(startCount(startWait));
    }

    // Update is called once per frame
    void Update()
    {
        if (IfGameStart && !gameover)
        {
            if (Input.GetButtonDown("SystemPause"))
            {
                if (!st.IfSystemPause)
                {
                    Debug.Log("Pause");
                    UIManager.Instance.ShowDesPanel("停止中");
                    st.gamestop(StopSystem.PauseState.SystemPause);
                }
                else
                {
                    UIManager.Instance.menuPanel.SetActive(false);
                    st.gamestop(StopSystem.PauseState.Resume);
                }
            }
        }
        

        if (ghostEnable)
        {
            distance_ghost_to_thief = getXZDistance(ght.gameObject, tf.gameObject);
            distance_player_to_ghost = getXZDistance(pc.gameObject, ght.gameObject);
        }

        distance_player_to_thief = getXZDistance(pc.gameObject, tf.gameObject);
        if (!gameover)
        {
            //終了チェック
            EndCheck();

            //宝が盗まれたらモデルをHideする
            if (tf.thiefState == Thief.ThiefState.HEAD_EXIT /*&& swordlight != null*/)
            {
                treasure.GetComponent<treasure>().sword.SetActive(false);
                //swordlight.Stop();
                //swordlight.Clear();
            }
        }
        else
        {
            //ゲームオーバー処理
            st.gamestop(StopSystem.PauseState.Normal);
            if(st.canStop)
                st.canStop = false;
            if (!iestart)
                StartCoroutine(ie());

        }
        
    }
    /// <summary>
    /// ゲームを終了するかをチェック
    /// </summary>
    void EndCheck()
    {
        //Debug.Log(distance_player_to_thief);
        
        if (tf.thiefState == Thief.ThiefState.END)
        {
            //UIManager.Instance.ShowDesPanel("泥棒が逃げました！");
            //text.text = "泥棒が逃げました！";
            //text.enabled = true;
            Debug.Log("泥棒が逃げました！");

            OverState = GameOverState.steal;
            ght.ghostState = Ghost_targeting.GhostState.GameOver;
            gameover = true;

        }
        //if (tf.thiefState == Thief.ThiefState.EXITED)
        //{
        //    //UIManager.Instance.ShowDesPanel("平和な夜ですね．．．");
        //    //text.text = "平和な夜ですね．．．";
        //    //text.enabled = true;
        //    Debug.Log("You Win!");
        //    OverState = GameOverState.arrest;
        //    ght.Gs = Ghost_targeting.GhostState.GameOver;
        //    gameover = true;
        //    //Invoke("gameOver(GameOverState.steal)", 2f);
        //}
        if (distance_player_to_thief <= checkdistance)
        {
            //UIManager.Instance.ShowDesPanel("逮捕成功!");
            //text.text = "逮捕成功!";
            //text.enabled = true;
            Debug.Log("You Win!");
            OverState = GameOverState.arrest;
            ght.ghostState = Ghost_targeting.GhostState.GameOver;
            gameover = true;
        }
        if (ghostEnable)
        {
            if (ght.ifHolyWaterAffect) return;
            if (distance_ghost_to_thief <= checkdistance)
            {
                //UIManager.Instance.ShowDesPanel("迷えば、敗れる");
                //text.text = "迷えば、敗れる";
                //text.enabled = true;
                Debug.Log("泥棒が殺人鬼に殺された！！");
                OverState = GameOverState.thiefDead;
                ght.ghostState = Ghost_targeting.GhostState.Kill;
                gameover = true;

            }
            if (distance_player_to_ghost <= checkdistance)
            {
                //UIManager.Instance.ShowDesPanel("死");
                //text.text = "死";
                //text.enabled = true;
                Debug.Log("死");
                OverState = GameOverState.policeDead;
                ght.ghostState = Ghost_targeting.GhostState.Kill;
                gameover = true;

            }
        }
    }

    /// <summary>
    /// ゲームオーバー処理
    /// </summary>
    /// <returns></returns>
    IEnumerator ie()
    {
        pc.CanPlayerMove = false;
        iestart = true;
        Time.timeScale = 1f;
        yield return new WaitForSeconds(2f);    //アニメーション時間を待ち
        gameOver();
    }

    void gameOver()
    {
        tf.thiefState = Thief.ThiefState.STOP;

        //条件によるEndingSceneが現す
        switch (OverState)
        {
            case GameOverState.arrest:
                endingscene = Instantiate(arrestCanvas);
                break;
            case GameOverState.thiefDead:
                endingscene = Instantiate(deadCanvas);
                break;
            case GameOverState.policeDead:
                endingscene = Instantiate(policeDeadCanvas);
                break;
            case GameOverState.steal:
                endingscene = Instantiate(stealCanvas);
                break;
            default:
                break;
        }

    }
    //IEnumerator startCount(float startTime)
    //{
    //    Time.timeScale = 0;
    //    for (int i = (int)startTime + 1; i >= 0; i--)
    //    {
    //        if (i > 0)
    //        {
    //            text.text = (i.ToString());
    //        }
    //        else
    //            text.text = "Start!!";
    //        Debug.Log(i + 1 + "..");
    //        yield return new WaitForSecondsRealtime(1f);
    //    }
    //    text.enabled = false;
    //    Debug.Log("start!!");
    //    IfGameStart = true;
    //    st.canStop = true;
    //    Time.timeScale = 1f;
    //    GimmickManager.Instance.GimmickFrag = true;
    //}
    public void playStartCount()
    {
        Time.timeScale = 0;
        StartCount.SetActive(true);
    }

    public void gameStart()
    {
        Debug.Log("start!!");
        IfGameStart = true;
        st.canStop = true;
        Time.timeScale = 1f;
        GimmickManager.Instance.GimmickFrag = true;
    }

    public float getXZDistance(GameObject a, GameObject b)
    {
        Vector3 ay0 = new Vector3(a.transform.position.x, 0, a.transform.position.z);
        Vector3 by0 = new Vector3(b.transform.position.x, 0, b.transform.position.z);
        float d0 = Vector3.Distance(ay0, by0);
        return d0;
        //float distance = Mathf.Sqrt((a.transform.position.x - b.transform.position.x) * (a.transform.position.x - b.transform.position.x) + (a.transform.position.z - b.transform.position.z) * (a.transform.position.z - b.transform.position.z));
        //return distance;
    }
    public float getXZDistance(Transform a, Transform b)
    {
        Vector2 ay0 = new Vector2(a.position.x, a.position.z);
        Vector2 by0 = new Vector2(b.position.x, b.position.z);
        float d0 = Vector2.Distance(ay0, by0);
        return d0;
        //float distance = Mathf.Sqrt((a.transform.position.x - b.transform.position.x) * (a.transform.position.x - b.transform.position.x) + (a.transform.position.z - b.transform.position.z) * (a.transform.position.z - b.transform.position.z));
        //return distance;
    }
    //public IEnumerator showTextWithSeconds(string msg,float seconds)
    //{
    //    Time.timeScale = 0;
    //    text.text = msg;
    //    text.enabled = true;
    //    yield return new WaitForSecondsRealtime(seconds);
    //    text.enabled = false;
    //    Time.timeScale = 1;
    //}

    
}
