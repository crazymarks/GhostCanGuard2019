using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Throw : MonoBehaviour
{
    public Slider AimSlider;
    public GameObject HolyWater;
    public float force=10;
    [Range(0, 10)]
    public int count = 1;       //残り弾数
    private bool IfActivated;
    stop st;
    // Start is called before the first frame update
    void Start()
    {
        st = GameManager.Instance.GetComponent<stop>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Send") && st.selectedObject == gameObject && (!IfActivated || st.SecondPhase == true))
        {
            throwHolyWater();
        }
    }

    void throwHolyWater(){
        if (count <= 0) {
            Debug.Log("弾が切れた");
            return;
        }
        if(!IfActivated)
            IfActivated = true;
        if (!st.SecondPhase)
        {
            st.SecondPhase = true;
            AimSlider.gameObject.SetActive(true);
            Debug.Log("Choose target");
            return;
        }
        if (!Input.GetButtonDown("Send")) return;
        GameObject holywater = Instantiate(HolyWater, new Vector3(gameObject.transform.position.x, 1, gameObject.transform.position.z), Quaternion.identity);
        Vector3 target = st.getCursorWorldPosition();
        transform.LookAt(new Vector3(target.x, gameObject.transform.position.y, target.z));
        Rigidbody rb = holywater.GetComponent<Rigidbody>();
        rb.AddForce(transform.forward * force, ForceMode.Impulse);
        st.gamestop();
        st.SecondPhase = false;
        IfActivated = false;
        count--;
    }
}
