using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalSE : MonoBehaviour
{
    public AudioClip clip;
    public AudioSource source;
    // Start is called before the first frame update
    public void click()
    {
        source.PlayOneShot(clip);

    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
