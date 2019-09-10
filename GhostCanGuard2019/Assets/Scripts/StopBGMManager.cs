using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(AudioSource))]
public class StopBGMManager : MonoBehaviour
{
    public AudioSource BGM;
    // Start is called before the first frame update
    void Awake()
    {
        BGM = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        switch (StopSystem.Instance.currentstate)
        {
            case StopSystem.PauseState.Normal:
                if(BGM.isPlaying)
                    BGM.Pause();
                break;
            case StopSystem.PauseState.ObserverMode:
                if (!BGM.isPlaying)
                {
                    BGM.Play();
                }
                BGM.volume = 1f;
                break;
            case StopSystem.PauseState.DirectionSelect:
                break;
            case StopSystem.PauseState.DescriptionOpen:
                BGM.volume = 0.5f;
                break;
            case StopSystem.PauseState.SystemPause:
                BGM.volume = 0.5f;
                break;
            default:
                break;
        }
    }
}
