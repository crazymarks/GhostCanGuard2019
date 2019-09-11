using UnityEngine;

public class TitleLight : MonoBehaviour
{

    public float speed = 0.2f;
    public Transform spotlight1;
    public Transform spotlight2;


    // Update is called once per frame
    void Update()
    {
        float rotatechange = Mathf.PingPong(Time.time * speed, 1);
        spotlight1.eulerAngles = Vector3.Lerp(new Vector3(-140f,90f,-90f),new Vector3(-115f,90f,-90f), rotatechange);
        spotlight2.eulerAngles = Vector3.Lerp(new Vector3(-65f, 90f, -90f), new Vector3(-40f, 90f, -90f), rotatechange);
    }
}
