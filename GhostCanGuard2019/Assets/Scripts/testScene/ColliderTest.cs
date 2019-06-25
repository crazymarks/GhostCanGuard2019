using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderTest : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "item") Debug.Log("EnteredCollider");
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.tag == "item") Debug.Log("InsideCollider");
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "item") Debug.Log("ExitedCollider");
    }
}
