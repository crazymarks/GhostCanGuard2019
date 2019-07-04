using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(EventTrigger))]
public class BreakingKabinGimmick : GimmickBase
{
    private WaitForSeconds waitForSeconds = new WaitForSeconds(1.0f);

    protected override void Start()
    {
        base.Start();
        GimmickEventSetUp(EventTriggerType.PointerDown, GimmickEventOpen);
    }
    private void OnTriggerEnter(Collider other)
    {
        switch(other.tag)
        {
            case "Player":
                PlayerManager.Instance.SetCurrentState(PlayerState.Slow);
                break;
            case "Thief":
                break;
        }
    }

    public void ClearBrokenKabin()
    {
        StartCoroutine(DeleteBrokenKabin());
    }

    IEnumerator DeleteBrokenKabin()
    {
        yield return waitForSeconds;

        Destroy(this.gameObject);
        GimmickManager.Instance.ClearGimmick();
    }
}
