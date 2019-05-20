using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseHit : MonoBehaviour
{
    GameObject highlighttrigger;
    Material material;
    [SerializeField]
    [Range(0f, 3f)]
    private float zoomRange = 0f;
    // Start is called before the first frame update
    void Start()
    {
        material = GetComponent<Renderer>().material;

        highlighttrigger = new GameObject("highttrigger");
        highlighttrigger.transform.position = transform.position;
        highlighttrigger.transform.localScale = transform.localScale + new Vector3(zoomRange, 0, zoomRange);
        highlighttrigger.transform.SetParent(transform);
        highlighttrigger.AddComponent<BoxCollider> ();
        highlighttrigger.layer = 10;
       
    }

   


    void MouseCheck()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray,out hit)&&hit.collider.gameObject==highlighttrigger)
        {
            material.SetFloat("_OutlineWidth", 1.3f);
            material.SetVector("_OutlineColor", new Vector4(1f, 1f, 0f, 0.8f));
            Debug.Log(name);
        }
        else material.SetFloat("_OutlineWidth", 1f);

    }

    private void Update()
    {
        MouseCheck();
    }

    //private void OnMouseOver()
    //{
        
    //    material.SetFloat("_OutlineWidth", 1.3f);
    //    Debug.Log(name);
    //}
    //private void OnMouseExit()
    //{
    //    material.SetFloat("_OutlineWidth", 1f);
    //}

}
