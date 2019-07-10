using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class HourseUI : MonoBehaviour
{
    [SerializeField]
    Button startButton;
    [SerializeField]
    Image image;
    [SerializeField]
    Hourse hourse;
    [SerializeField]
    Image panel;
    // Start is called before the first frame update
    void Start()
    {
        if (startButton == null)
        {
            startButton = transform.Find("Start").GetComponent<Button>();
        }
        if (image == null)
        {
            image = transform.Find("GetInput").GetComponent<Image>();
        }
        if (hourse == null)
        {
            hourse = GetComponentInParent<Hourse>();
        }
        if (panel == null)
        {
            panel = transform.Find("Panel").GetComponent<Image>();
        }
        image.enabled = false;
        panel.gameObject.SetActive(false);
    }

    //void hourseMoveUP()
    //{
    //    hourse.Active(Vector3.forward);
    //}
    
    //public void ClickUP()
    //{
    //    hourse.GimmickUIClose();
    //    GimmickManager.Instance.SetGimmickAction(hourseMoveUP);
    //}
    //void hourseMoveDown()
    //{
    //    hourse.Active(Vector3.back);
    //}

    //public void ClickDown()
    //{
    //    hourse.GimmickUIClose();
    //    GimmickManager.Instance.SetGimmickAction(hourseMoveDown);
    //}
    //void hourseMoveLeft()
    //{
    //    hourse.Active(Vector3.left);
    //}

    //public void ClickLeft()
    //{
    //    hourse.GimmickUIClose();
    //    GimmickManager.Instance.SetGimmickAction(hourseMoveLeft);
    //}
    //void hourseMoveRight()
    //{
    //    hourse.Active(Vector3.right);
    //}

    //public void ClickRight()
    //{
    //    hourse.GimmickUIClose();
    //    GimmickManager.Instance.SetGimmickAction(hourseMoveRight);
    //}
    public void ClickUIStart()
    {
        startButton.gameObject.SetActive(false);
        //panel.gameObject.SetActive(true);

    }
}
