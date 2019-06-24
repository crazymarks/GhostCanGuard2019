using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GimmickShaderSample : UnityEngine.MonoBehaviour
{
    private Material material;
    private Vector3 mousePos;

    // Start is called before the first frame update
    void Start()
    {
        this.material = gameObject.GetComponent<Renderer>().material;
        Debug.Log(material.name);
    }

    // Update is called once per frame
    void Update()
    {
        mousePos = Input.mousePosition;
        mousePos.z = 11f;
        Vector3 wmp = Camera.main.ScreenToWorldPoint(mousePos);

        this.gameObject.transform.position = wmp;
        material.SetVector("_MousePosition", wmp);
    }
}
