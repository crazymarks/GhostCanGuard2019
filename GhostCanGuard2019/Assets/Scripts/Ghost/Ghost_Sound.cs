using UnityEngine;
[RequireComponent(typeof(AudioSource))]
public class Ghost_Sound : MonoBehaviour
{
    
    //SE
    [SerializeField] AudioClip moveSE = null;
    AudioSource audioSource;
    float volume;
    // Start is called before the first frame update
    void Awake()
    {
        if (moveSE == null)
        {
            Debug.Log("moveSE未指定");
            Destroy(this);
        }
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = moveSE;
        volume = audioSource.volume;
    }
    private void Update()
    {
        audioSource.pitch = Time.timeScale;
        audioSource.volume = Time.timeScale * volume;
    }

    public void playSE()
    {
        audioSource.Play();
    }
    public void stopSE()
    {
        audioSource.Stop();
    }
}
