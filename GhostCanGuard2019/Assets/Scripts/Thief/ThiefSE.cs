using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThiefSE : MonoBehaviour
{
    Thief thief;
    //SE
    [SerializeField] AudioClip moveSE;
    AudioSource audioSource;
    // Start is called before the first frame update
    void Start()
    {
        thief = GetComponent<Thief>();
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = moveSE;
    }

    // Update is called once per frame
    void Update()
    {
        if (!audioSource.isPlaying)
        {
            if (thief.thiefState != Thief.ThiefState.STOP)
                audioSource.Play();

        }
        else
        {
            if (thief.thiefState == Thief.ThiefState.ESCAPE) {
                audioSource.Stop();
            }
        }
    }
}
