using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DescriptionUIChange : MonoBehaviour
{

    [SerializeField]
    GameObject InfoUI;
    [SerializeField]
    GameObject ActionUI;
    [SerializeField]
    GameObject CancelUI;

    bool cancelUIon = false;

    // Start is called before the first frame update
    void Start()
    {
        CancelUI.transform.position = ActionUI.transform.position;
        cancelUIon = CancelUI.activeSelf;
    }

    void CancelUIShow()
    {
        InfoUI.SetActive(false);
        ActionUI.SetActive(false);
        CancelUI.SetActive(true);
        cancelUIon = true;
    }
    void CancelUIClose()
    {
        InfoUI.SetActive(true);
        ActionUI.SetActive(true);
        CancelUI.SetActive(false);
        cancelUIon = false;
    }

    public void DescriptionOnOff()
    {
        if (!cancelUIon)
            CancelUIShow();
        else
            CancelUIClose();
    }

}
