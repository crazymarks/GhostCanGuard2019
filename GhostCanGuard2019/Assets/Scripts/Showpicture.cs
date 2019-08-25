using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Showpicture : MonoBehaviour
{
    // Start is called before the first frame update


    public GameObject News = null;
    public GameObject Button1 = null;

    public void start()
    {
        News.SetActive(false);
        Button1.SetActive(false);
        StartCoroutine(WaitBeforshow());
    }

    // Update is called once per frame
    private void showpicture()
    {
        News.SetActive(true);
        Button1.SetActive(true);
    }

    IEnumerator WaitBeforshow()
    {
        News.SetActive(true);
        yield return new WaitForSeconds(10f);
        Button1.SetActive(true);
    }
}
