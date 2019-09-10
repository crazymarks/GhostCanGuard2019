using UnityEngine;
[RequireComponent(typeof(AudioSource))]
public class BGMManager : MonoBehaviour
{
    AudioSource BGM;
    // Start is called before the first frame update
    void Awake()
    {
        BGM = GetComponent<AudioSource>();
        DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        if (LoadScene.GetCurrentSceneName() != "TitleScene" && LoadScene.GetCurrentSceneName() !="Video")
        {
            if (GameManager.Instance.gameObject.GetComponent<StopBGMManager>().BGM.isPlaying)
            {
                if (BGM.isPlaying)
                    BGM.Pause();
            }
            else
            {
                if (!BGM.isPlaying)
                    BGM.Play();
            }
        }
        if(LoadScene.GetCurrentSceneName() == "Video")
        {
            if (BGM.isPlaying)
                BGM.Pause();
        }
    }
}
