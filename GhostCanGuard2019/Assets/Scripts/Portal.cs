using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{
    [SerializeField]
    bool IfEnable = false;
    public float PortDlay = 0;
    public GameObject[] PortalPair = new GameObject[2]; //入口と出口をペアーにする
    Vector3 LastPos;
    int portalpairsize = 2;
    int portalId = 0;
    
    private void Port()
    {
        GameObject mono = GameObject.FindGameObjectWithTag("Player");
        mono.transform.position = PortalPair[(portalId+1)% portalpairsize].transform.position;
        portalId++;
    }
    private void OnTriggerEnter(Collider collision)
    {
        Debug.Log("Port");
        // 物体がトリガーに接触しとき、１度だけ呼ばれる
        Port();
    }
    // Start is called before the first frame update
    void Start()
    {
       
     
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
