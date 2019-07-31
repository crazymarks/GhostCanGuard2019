using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(AudioSource))]
public class PlayerSe : MonoBehaviour
{
    Rigidbody rb;
    PlayerControl player;
    //SE
    [SerializeField] AudioClip moveSE;
    AudioSource audioSource;
    // Start is called before the first frame update
    void Start()
    {
        player = GetComponent<PlayerControl>();
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = moveSE;
    }

    public void playSE(AudioClip SEName)
    {
        audioSource.clip = SEName;
        if (!audioSource.isPlaying)
            audioSource.Play();
        else
            audioSource.Stop();
    }


    // Update is called once per frame
    void Update()
    {
        audioSource.pitch = Mathf.Abs(player.speed / 5);
        if (!audioSource.isPlaying)
        {
            if (rb.velocity.magnitude > 0.2)
                audioSource.Play();
        }
        else
        {
            if (rb.velocity.magnitude <=0.2)
            {
                audioSource.Stop();
            }
        }
    }

}


