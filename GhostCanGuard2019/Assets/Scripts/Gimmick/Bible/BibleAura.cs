using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BibleAura : MonoBehaviour
{
    Ghost_targeting gt;
    public float buffTime;
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Ghost")
        {
            gt = other.gameObject.GetComponent<Ghost_targeting>();

            gt.bible(buffTime, this.transform);
        }

    }
}
