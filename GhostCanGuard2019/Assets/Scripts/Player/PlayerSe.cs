using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSe : MonoBehaviour
{
    public Rigidbody rb;
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

    // Update is called once per frame
    void Update()
    {
        audioSource.pitch = Mathf.Abs(player.speed / 5);
        if (!audioSource.isPlaying)
        {
            if (rb.velocity != Vector3.zero)
                audioSource.Play();

        }
        else
        {
            if (rb.velocity == Vector3.zero)
            {
                audioSource.Stop();
            }
        }
    }

}


