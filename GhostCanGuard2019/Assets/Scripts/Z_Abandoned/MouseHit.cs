using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseHit : MonoBehaviour
{
    GameObject highlighttrigger;

    Camera MainCamera;

    //Material material;
    [SerializeField]
    [Range(0f, 3f)]
    private float zoomRange = 0f;
    // Start is called before the first frame update
    void Start()
    {
        //material = GetComponent<Renderer>().material;
        MainCamera=Camera.main;
        MainCamera.GetComponent<OutLineCamera>();
       
        highlighttrigger = new GameObject("highlighttrigger");
        highlighttrigger.transform.position = transform.position;
        highlighttrigger.transform.localScale = transform.localScale + new Vector3(zoomRange, 0, zoomRange);
        highlighttrigger.transform.SetParent(transform);
        highlighttrigger.AddComponent<BoxCollider> ();
        highlighttrigger.layer = 10;
        highlighttrigger.tag = "Default Banned By Portal";
        
       
    }


    void MouseCheck()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit,LayerMask.NameToLayer("highlighttrigger")) && hit.collider.gameObject == highlighttrigger)
        {
            Camera.main.GetComponent<OutLineCamera>().enabled = true;
            ChangeLayer(transform, "outline");
            
            //material.SetFloat("_OutlineWidth", 1.3f);
            // material.SetVector("_OutlineColor", new Vector4(1f, 1f, 0f, 0.8f));
            Debug.Log(name);
        }
        else
        {
            //Camera.main.GetComponent<OutLineCamera>().enabled = false;
            ChangeLayer(transform, "Default");
            //else material.SetFloat("_OutlineWidth", 1f);
        }
    }

    private void Update()
    {
        MouseCheck();
    }


    void ChangeLayer(Transform trans,string targetLayer)
    {
        if (LayerMask.NameToLayer(targetLayer) == -1)
        {
            Debug.Log("layer dosen't exist");
            return;
        }
        trans.gameObject.layer = LayerMask.NameToLayer(targetLayer);
        foreach(Transform child in trans)
        {
            ChangeLayer(child, targetLayer);
        }
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
