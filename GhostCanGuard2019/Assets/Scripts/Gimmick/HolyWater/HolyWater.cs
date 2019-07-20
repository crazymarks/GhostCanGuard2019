using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class HolyWater : MonoBehaviour
{
    int count = 0;      //残り利用回数
    public int Count { get { return count; } /*set { count = value; }*/ }   //インターフェース
    public float buffTime = 5f;
    public float lifeTime = 5f;
    

    GameObject holywater;
    GameObject cup;




    // Start is called before the first frame update
    private void Start()
    {
        Destroy(gameObject, lifeTime);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "Player") return;
        if (collision.gameObject.tag == "Ghost")
        {
            Destroy(gameObject);
            collision.gameObject.GetComponent<Ghost_targeting>().HolyWater(buffTime);
        }
    }

}
