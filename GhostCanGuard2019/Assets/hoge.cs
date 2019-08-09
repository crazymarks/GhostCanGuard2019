using UnityEngine;
using System.Collections;

public class hoge : MonoBehaviour
{
    IEnumerator routine;
    // Use this for initialization
    void Start()
    {
        routine = coroutine();
        StartCoroutine(routine);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.X))
        {
            StopCoroutine("coroutine");
            StartCoroutine("coroutine");
        }
    }

    IEnumerator coroutine()
    {
        Debug.Log("start");
        yield return new WaitForSeconds(1f);
        Debug.Log("1s");
        yield return new WaitForSeconds(1f);
        Debug.Log("2s");
        yield return new WaitForSeconds(1f);
        Debug.Log("3s");
        yield return new WaitForSeconds(1f);
        Debug.Log("4s");
        yield return new WaitForSeconds(1f);
        Debug.Log("5s");
        yield return new WaitForSeconds(1f);
    }
}