using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostSE : MonoBehaviour
{
    
    Ghost_targeting ghost_Targeting;
    //SE
    [SerializeField] AudioClip moveSE;
    AudioSource audioSource;
    // Start is called before the first frame update
    void Start()
    {
        ghost_Targeting = GetComponent<Ghost_targeting>();
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = moveSE;
    }

    // Update is called once per frame
    void Update()
    {
        if (!audioSource.isPlaying)
        {
            if (ghost_Targeting.Gs != Ghost_targeting.GhostState.HolyWater_Affected)
                audioSource.Play();

        }
        else
        {
            if (ghost_Targeting.Gs == Ghost_targeting.GhostState.HolyWater_Affected)
            {
                audioSource.Stop();
            }
        }
    }
}
