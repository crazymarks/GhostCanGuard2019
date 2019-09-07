using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ParticalDelaytime : MonoBehaviour
{
    [SerializeField]
    ParticleSystem pObject;
    //bool ifenabled = false;
    //パーティクルが停止される時間を指定
    public float ParticleDelaytime = .2f;
    // Start is called before the first frame update
    void Awake()
    {
        //ifenabled = true;
        pObject = GetComponent<ParticleSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        //if (Input.GetKeyDown(KeyCode.Escape))
        //{
        //    if (ifenabled == true)
        //    {
        //        pObject.Stop();
        //        pObject.Clear();
        //        ifenabled = false;
        //    }
        //    else
        //    {
        //        pObject.Play();
        //        ifenabled = true;
        //    }

        //}


        if (Input.GetKeyDown(KeyCode.Escape) && pObject.isStopped)
        {
            Debug.Log("b");
            pObject.gameObject.SetActive(true);
            pObject.Simulate(4.0f, true, false);
            pObject.Play();
            StartCoroutine(delay(ParticleDelaytime, () =>
            {
                pObject.gameObject.SetActive(false);
            }));

            Debug.Log("a");

        }
    }

    IEnumerator delay(float waitTime, UnityAction action)
    {
        yield return new WaitForSeconds(waitTime);
        action();
    }

}


