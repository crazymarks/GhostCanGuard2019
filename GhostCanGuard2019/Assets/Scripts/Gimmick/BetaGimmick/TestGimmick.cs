using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;

public class TestGimmick : GimmickBase
{
    protected override void Start()
    {
    }

    public void OnClickGimmickStart()
    {
        GimmickManager.Instance.SetGimmickAction(FlyAwayGimmick);
    }

    private void FlyAwayGimmick()
    {
        Debug.Log("Gimmick start");
        GimmickManager.Instance.ClearGimmick();
    }
}
