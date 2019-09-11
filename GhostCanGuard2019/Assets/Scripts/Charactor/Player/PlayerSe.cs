using UnityEngine;
[RequireComponent(typeof(AudioSource))]
public class PlayerSe : MonoBehaviour
{
    Rigidbody rb;
    PlayerControl player;
    //SE
    [SerializeField] AudioClip moveSE = null;
    AudioSource audioSource;
    AudioSource specialSe;
    // Start is called before the first frame update
    void Start()
    {
        if (moveSE == null)
        {
            Debug.Log("moveSE未指定");
            Destroy(this);
        }
        player = GetComponent<PlayerControl>();
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = moveSE;
    }

    public void playSE(AudioClip SEName)
    {
        if(!specialSe)
            specialSe = gameObject.AddComponent<AudioSource>();
        specialSe.clip = SEName;
        specialSe.PlayOneShot(SEName);
    }


    // Update is called once per frame
    void Update()
    {
        audioSource.pitch = Mathf.Abs(player.speed / 5);
        if (!audioSource.isPlaying)
        {
            if (rb.velocity.magnitude > 0.2 && player.CanPlayerMove)
                audioSource.Play();
        }
        else
        {
            if (rb.velocity.magnitude <=0.2 || !player.CanPlayerMove)
            {
                audioSource.Stop();
            }
        }
    }

}


