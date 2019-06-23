using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{
    [SerializeField]
    private bool IfEnable = false;              //ゲートが開いているかどうかの状態

    public float PortDlay = 0f;              //転送遅延時間

    [SerializeField]
    private Portal PortDestination;     //目標ゲート

    private bool IfPorted = false;      //このゲートがいま使った(目標として)かどうかを判定

    //[SerializeField]
    //private List<string> BannedTag = new List<string>()
    //{
    //    "Default Banned By Portal"
    //};     //転送できない物のタグ
    [SerializeField]
    private List<string> PortabeTag = new List<string>()
    {
        "Player"
    };     //転送できる物のタグ

    void Awake()
    {
        IfEnable = true;
        IfPorted = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        
        if (IfEnable && !IfPorted)        //開いている且つ目標じゃない
        {
            Debug.Log("Enabled Portal IN TelePort IN " + PortDlay + " Seconds");
            Port(other.gameObject);

        }

    }
    private void OnTriggerExit(Collider other)
    {
        IfPorted = false;   //転送者が離れたら、このゲートをまた使う;
    }


    public void PortOnOff()       //ゲートを開けるまたは閉じる
    {
        IfEnable = !IfEnable;
        //if (!IfEnable)
        //{
        //    IfEnable = true;
        //}
        //else
        //    IfEnable = false;
    }


    //private void Port(GameObject obj)
    //{
    //    for (int i = 0; i < BannedTag.Count; i++)
    //    {
    //        if (obj.tag == BannedTag[i])
    //        {
    //            Debug.Log("Banned Object IN, Cannot TelePort This Object");
    //            return;
    //        }
    //    }
    //    StartCoroutine(Teleport(obj));
    //    PortDestination.IfPorted = true;  // 目標ゲートを使った状態に設定
    //    Debug.Log("Port");
    //}
    private void Port(GameObject obj)
    {
        for (int i = 0; i < PortabeTag.Count; i++)
        {
            if (obj.tag == PortabeTag[i])
            {
                StartCoroutine(Teleport(obj));
                PortDestination.IfPorted = true;  // 目標ゲートを使った状態に設定
                Debug.Log("Port");

            }
            else
                Debug.Log("Banned Object IN, Cannot TelePort This Object");
            return;
        }
    }
    IEnumerator Teleport(GameObject obj)
    {
       
                //MayDo:animation tele_Start_Anime;
        yield return new WaitForSeconds(PortDlay);
        if (PortDestination != null)
        {
            GameObject mono = obj;
            mono.transform.position = PortDestination.transform.position;
        }
        else
            Debug.Log("UnSet PortDestination, TelePort Failed");
                //MayDo:anmation tele_End_Anime:

    }



    //public void addBanTag(string tag)
    //{
    //    BannedTag.Add(tag);
    //}
    //public void RemoveBanTag(string tag)
    //{
    //    BannedTag.Remove(tag);
    //}

    public void addPortableTag(string tag)
    {
        PortabeTag.Add(tag);
    }
    public void RemovePortableTag(string tag)
    {
        PortabeTag.Remove(tag);
    }
}
