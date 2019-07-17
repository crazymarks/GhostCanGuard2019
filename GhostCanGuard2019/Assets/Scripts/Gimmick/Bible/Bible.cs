using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Bible : GimmickBase
{
   
    public float buffTime = 5f;
    public float coolDownTime = 5f;
    public float auraTime = 5f;
    public float auraRadius = 5f;

    bool isOpen;
    bool canOpen;
    float startTime;
    SphereCollider collision;

    ParticleSystem aura;

    GameObject obj;
    Ghost_targeting gt;
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();

        collision = GetComponent<SphereCollider>();
        if (collision != null)
        {
            collision.enabled = false;
            collision.radius = auraRadius;
        }
        if (aura == null)
        {
            aura = GetComponentInChildren<ParticleSystem>();
        }
        aura.Stop();
        aura.Clear();
        isOpen = false;
        canOpen = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Ghost" && isOpen)
        {
            gt = other.gameObject.GetComponent<Ghost_targeting>();
            gt.bible(buffTime);
        }
        
    }

    


    private void open()
    {
        if (canOpen)
        {
            StartCoroutine(AuraON());
        }
        else
        {
            if (isOpen)
                Debug.Log("起動中です");
            else
                Debug.Log("準備中です");
        }
        st.gamestop(); 
        //GimmickUIClose();

    }
    IEnumerator AuraON()
    {
        isOpen = true;
        collision.enabled = true;
        aura.Play();
        canOpen = false;
        yield return new WaitForSeconds(auraTime);
        isOpen = false;
        aura.Stop();
        collision.enabled = false;
        yield return new WaitForSeconds(coolDownTime);
        canOpen = true;
        Debug.Log("Ready to ReUse");
    }

    public void ClickUIStart()
    {
        GimmickManager.Instance.SetGimmickAction(open);
        GimmickUIsOnOff(false);
    }
    private void Update()
    {
        if (Input.GetButtonDown("Send") && st.selectedObject == gameObject)
        {
            open();
        }
    }
}
