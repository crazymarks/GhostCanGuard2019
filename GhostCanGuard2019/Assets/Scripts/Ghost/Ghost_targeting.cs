using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Ghost_targeting : MonoBehaviour
{

    public enum GhostState
    {
        Chasing_Player,
        Chasing_Thief,
        Patrol,
        Bible_Affected,
        HolyWater_Affected,
        GameOver,
        Kill
    }
    [Header("鬼の状態")]
    public GhostState ghostState;

    //SE
    Ghost_Sound SE;

    //Animation
    GhostAnimationController GhostAnim;

   

    //目標を追う速度
    public float chasingSpeed = 5f;
    //体を回す速度
    public float turnSpeed = 0.5f;
    //パトロール速度
    public float walkSpeed = 2f;
    
    //プレイヤーを追う距離
    public float distanceOfChasingPlayer = 10f;
    //追いかけを止める距離
    public float distanceOfGiveUpChase = 15f;
    //パトロールの最大範囲
    public float maxPatrolRange=5f;
    //パトロールの間隔
    public float waitTime = 5f;

    //目標のゲームオブジェクト
    GameObject targetobj;
    //泥棒のゲームオブジェクト
    GameObject thief;
    //プレイヤーのゲームオブジェクト
    GameObject player;
    //泥棒のrigibody(速度を取得ため)
    Rigidbody trb;
    //泥棒が隠しますかどうか
    bool ifThiefHide;

    //プレイヤーまでの距離
    float distancetoPlayer;
    //泥棒までの距離
    float distancetoThief;
    //目標までの回転
    Quaternion targetQuaternion;

    //目標を失う位置(パトロールの範囲判定用)
    Vector3 missingPosition;
    //パトロールの目標位置
    Vector3 patroPosition;
    //前回のパトロールが始まる時間
    float lastActTime;
    //パトロールの距離(パトロールの範囲判定用)
    float patrolDistance = 0f;


    //目標の瞬時位置(バイブルに影響された時用)
    Vector3 targetpos;
    //バイブルに影響されたが
    public bool ifBibleAffect = false;

    public bool ifHolyWaterAffect = false;

    bool GameOver = false;


    // Start is called before the first frame update
    void Start()
    {
        //デフォルトでプレイヤーが設定されてない場合
        if (player == null)
        {
            try
            {
                player = GameObject.FindGameObjectWithTag("Player");
            }
            catch (System.NullReferenceException)
            {
                Debug.Log("プレイヤー未発見" + name);
                Destroy(gameObject);
            }
            
        }
        //デフォルトで泥棒が設定されてない場合
        if (thief == null)
        {
            try
            {
                thief = GameObject.FindGameObjectWithTag("Thief");
            }
            catch (System.NullReferenceException)
            {
                Debug.Log("Thief未発見" + name);
                Destroy(gameObject);
            }
           
        }
        //泥棒が見つかったら
        if (thief != null)
        {
            trb = thief.GetComponent<Rigidbody>();
            ifThiefHide = thief.GetComponent<thiefHide>().ifHide;
        }

        GhostAnim = GetComponentInChildren<GhostAnimationController>();
        SE = GetComponent<Ghost_Sound>();
        SE.playSE();
        GameOver = false;
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!gameObject.activeSelf || GameOver)
            return;
        switch (ghostState)
        {
            case GhostState.Chasing_Player:
                lastActTime = Time.time;
                move(player.transform.position, chasingSpeed, Vector3.zero);
                break;
            case GhostState.Chasing_Thief:
                lastActTime = Time.time;
                move(thief.transform.position, chasingSpeed, trb.velocity.normalized);
                break;
            case GhostState.Patrol:
                patrol();
                break;
            case GhostState.Bible_Affected:
                move(targetpos, -chasingSpeed, Vector3.zero);
                break;
            case GhostState.HolyWater_Affected:
                lastActTime = Time.time;
                move(gameObject.transform.position, 0, Vector3.zero);
                break;
            case GhostState.GameOver:
                GameOverUpdate();
                return;
            case GhostState.Kill:
                KillUpdate();
                return;
            default:
                break;
        }
        
        //目標を検知
        targetCheck();
        
    }
    //private void Update()
    //{
        
    //}
    /// <summary>
    /// 終了時の処理
    /// </summary>
    void GameOverUpdate()
    {
        if (!GameOver)
        {
            GameOver = true;
            SE.stopSE();
            StopAllCoroutines();
            GetComponentInChildren<Animator>().speed = 0;
        }
        else return;
    }
    private void KillUpdate()
    {
        if (!GameOver)
        {
            GhostAnim.SetGhostAnimation(GhostAnimator.SJK_Kill);
            GameOver = true;
            SE.stopSE();
            StopAllCoroutines();
        }
    }

    /// <summary>
    /// 殺人鬼を移動する
    /// </summary>
    /// <param name="target">移動先位置</param>
    /// <param name="speed">移動速度</param>
    /// <param name="advance_speed">目安追跡ポイントを計算</param>
    void move(Vector3 target,float speed, Vector3 advance_speed)
    {
        target.y = 0;
        if((target - transform.position).sqrMagnitude < 0.2f)  return;//ターゲットに近づく時に止まる
        
        
        //毎フレイムの移動距離を計算する
        Vector3 moveSpeed = (target + advance_speed - transform.position).normalized * speed * Time.deltaTime;
        //壁を無視する移動から、transformで位置を操作する
        transform.position += moveSpeed;
        //Debug.Log(moveSpeed);
        if (target - transform.position != Vector3.zero)
            //目標までの回転を計算する
            targetQuaternion = Quaternion.LookRotation(moveSpeed, Vector3.up);
        //slrepで回転する
        transform.rotation = Quaternion.Slerp(transform.rotation, targetQuaternion, turnSpeed);
    }

    /// <summary>
    /// パトロール
    /// </summary>
    void patrol()
    {
        
        if (Time.time - lastActTime > waitTime)　//前回の行動に一定時間を経過すると
        {
            generateRandomTarget();　//新しパトロール目標を生成する
            lastActTime = Time.time + (patroPosition-transform.position).magnitude / walkSpeed;　//パトロールの終了時間を計算する
        }
        move(patroPosition ,walkSpeed, Vector3.zero);//移動する
        

    }
    /// <summary>
    /// パトロール範囲内でランダムの目標を生成する
    /// </summary>
    void generateRandomTarget()
    {
        //パトロール距離を生成する
        patrolDistance = -maxPatrolRange / (UnityEngine.Random.Range(0f, maxPatrolRange - 1f) - maxPatrolRange);
        //Debug.Log(patrolDistance);
        //パトロール方向を生成する
        Vector2 point = UnityEngine.Random.insideUnitCircle * patrolDistance;
        //パトロールの目標を計算する
        patroPosition = new Vector3(transform.position.x + point.x, transform.position.y, transform.position.z + point.y);
        if ((patroPosition - missingPosition).magnitude > maxPatrolRange) //最大距離を超える時
        {
            //Debug.Log("離れすぎから、もう一度生成します");
            generateRandomTarget();
        }
        
    }
    /// <summary>
    /// 目標を検知
    /// </summary>
    void targetCheck()
    {
        if (ifBibleAffect||ifHolyWaterAffect) return;
        //オブジェクトがない時
        if (thief == null || player == null)
            return;
        //デフォルトで泥棒を追う
        if (targetobj == null && !ifThiefHide)
        {
            ghostState = GhostState.Chasing_Thief;
            targetobj = thief;
        }
        distancetoPlayer = Vector3.Distance(transform.position, player.transform.position);
        //Debug.Log(distancetoPlayer);
        distancetoThief = Vector3.Distance(transform.position, thief.transform.position);
        if (!ifThiefHide)  //泥棒が隠しない場合
        {
            if (distancetoPlayer < distanceOfChasingPlayer && distancetoPlayer < distancetoThief)
            {
                //プレイヤーが泥棒より殺人鬼に近づくかつ、殺人鬼の警戒範囲内に入る
                ghostState = GhostState.Chasing_Player;
                targetobj = player;
            }

            if (distancetoPlayer > distanceOfGiveUpChase || distancetoPlayer > distancetoThief)
            {
                //プレイヤーが泥棒より殺人鬼に離れた場合、または殺人鬼の最大追跡範囲に出す
                ghostState = GhostState.Chasing_Thief;
                targetobj = thief;
            }
        }
        else  //泥棒が隠す場合
        {
            if (targetobj == thief)  //最初に目標をnullにします
            {
                targetobj = null;
                missingPosition = transform.position;
                ghostState = GhostState.Patrol;
                //isTargeting = false;
            }
            if (distancetoPlayer < distanceOfChasingPlayer)
            {
                ghostState = GhostState.Chasing_Player;
                //isTargeting = true;
                //targetobj = player;
            }

            if (distancetoPlayer > distanceOfGiveUpChase)
            {
                targetobj = null;
                ghostState = GhostState.Patrol;
                //isTargeting = false;
            }
        }
    }
    /// <summary>
    /// バイブルの効果
    /// </summary>
    /// <param name="time">効果時間</param>
    public void bible(float time, Transform bible)
    {
        targetobj = bible.gameObject;
        StartCoroutine(bibleEffect(time));
    }
    IEnumerator bibleEffect(float time)
    {
        ifBibleAffect = true;
        ghostState = GhostState.Bible_Affected;
        targetpos = targetobj.transform.position;
        //Debug.Log("bible affected");
        yield return new WaitForSeconds(time);
        ifBibleAffect = false;
    }

    public void HolyWater(float time)
    {
        if (!ifHolyWaterAffect)
            StartCoroutine(HolyWaterEffect(time));
    }
    IEnumerator HolyWaterEffect(float time)
    {
        SE.stopSE();                                        //移動SE停止
        ifHolyWaterAffect = true;
        ghostState = GhostState.HolyWater_Affected;　　　　　　　　//状態切り替わる
        GhostAnim.SetGhostAnimation(GhostAnimator.Down);   //アニメーション設定
        Debug.Log("HolyWater affected");
        yield return new WaitForSeconds(time - 1.533f);     
        GhostAnim.SetGhostAnimation(GhostAnimator.StandUp);  //アニメーション設定
        yield return new WaitForSeconds(1.533f);             //立て直し時間を待ち
        GhostAnim.SetGhostAnimation(GhostAnimator.Walk);    //アニメーション設定
        ifHolyWaterAffect = false;
        SE.playSE();                                        //移動SE再生
    }
}
