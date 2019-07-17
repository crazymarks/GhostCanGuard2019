using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class HolyWater : GimmickBase
{
    int count = 0;      //残り利用回数
    public int Count { get { return count; } /*set { count = value; }*/ }   //インターフェース




    GameObject holywater;
    GameObject cup;




    // Start is called before the first frame update
    override protected void Start()
    {
        base.Start();

        
            try
            {
                transform.SetParent(GameObject.FindGameObjectWithTag("Player").transform);
                transform.position = transform.parent.position;
            }
            catch (System.NullReferenceException)
            {
                Debug.Log("プレイヤー未発見" + name);
            }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
