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



    // Start is called before the first frame update
    void Start()
    {
        if (ght != null)
        {
            ghostEnable = true;
        }
       
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
            Debug.Log(distance_player_to_thief);
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
        ldc.loadScene("TitleScene");
    }
    float getXZDistance(GameObject a, GameObject b)
    {
        float distance = Mathf.Sqrt((a.transform.position.x - b.transform.position.x) * (a.transform.position.x - b.transform.position.x) + (a.transform.position.z - b.transform.position.z) * (a.transform.position.z - b.transform.position.z));
        return distance;
    }
}
