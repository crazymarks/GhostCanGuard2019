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
        if (CancelUI != null)
        {
            CancelUI.transform.position = ActionUI.transform.position;
            cancelUIon = CancelUI.activeSelf;
        }
    }

    void CancelUIShow()
    {
        if(InfoUI!=null)
            InfoUI.SetActive(false);
        if(ActionUI!=null)
            ActionUI.SetActive(false);
        CancelUI.SetActive(true);
        cancelUIon = true;
    }
    void CancelUIClose()
    {
        if (InfoUI != null)
            InfoUI.SetActive(true);
        if (ActionUI != null)
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

    public void ActionUIHide()
    {
        if(ActionUI.activeSelf)
            ActionUI.SetActive(false);
    }
    public void ActionUIShow()
    {
        if (!ActionUI.activeSelf)
            ActionUI.SetActive(true);
    }
}
