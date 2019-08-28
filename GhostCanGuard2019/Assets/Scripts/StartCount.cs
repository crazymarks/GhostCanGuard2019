using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartCount : MonoBehaviour
{
    private void OnDestroy()
    {
        GameManager.Instance.gameStart();
    }
    
    void Destroythis()
    {
        Destroy(gameObject);
    }
}
