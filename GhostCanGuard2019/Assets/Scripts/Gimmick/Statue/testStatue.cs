using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class testStatue : GimmickBase
{
    public float thrust;
    public Rigidbody rb;

    protected override void Start()
    {
        base.Start();
        rb = GetComponent<Rigidbody>();
        GimmickEventSetUp(EventTriggerType.PointerDown, GimmickEventOpen);
    }

    // for buttons
    public void FallRight()
    {
        rb.AddForce(transform.right * thrust);
        GimmickUIClose();
    }

    public void FallLeft()
    {
        rb.AddForce(-transform.right * thrust);
        GimmickUIClose();
    }

    public void FallUpwards()
    {
        rb.AddForce(transform.forward * thrust);
        GimmickUIClose();
    }

    public void FallDownwards()
    {
        rb.AddForce(-transform.forward * thrust);
        GimmickUIClose();
    }

    public void CloseUI()
    {
        GimmickUIClose();
    }
}
