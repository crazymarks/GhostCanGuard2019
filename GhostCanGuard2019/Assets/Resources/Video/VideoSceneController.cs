using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VideoSceneController : MonoBehaviour
{
    public videocontroller video;
    bool haveLoad = false;
    // Start is called before the first frame update
    void Start()
    {
        haveLoad = false;
        video.videoPlayer.targetCameraAlpha = 1;
        video.videoPlayer.SetDirectAudioVolume(0, video.videoPlayer.targetCameraAlpha);
        video.stopVideo();
        video.playVideo();
    }

    // Update is called once per frame
    void Update()
    {
        if (haveLoad) return;
        if (Input.anyKeyDown || !video.videoPlayer.isPlaying)
        {
            haveLoad = true;
            StartCoroutine(fadeOut());
        }

    }

    IEnumerator fadeOut()
    {
        while (true)
        {
            video.videoPlayer.targetCameraAlpha *= 0.96f;
            video.videoPlayer.SetDirectAudioVolume(0, video.videoPlayer.targetCameraAlpha);
            yield return new WaitForEndOfFrame();
            if (video.videoPlayer.targetCameraAlpha < 0.05f)
            {
                video.stopVideo();
                LoadScene.loadScene("Cp1");
                Destroy(gameObject);
                yield break;
            }
        }
        
    }
}
