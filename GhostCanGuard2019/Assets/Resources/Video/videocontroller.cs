using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class videocontroller : MonoBehaviour
{

    public VideoPlayer videoPlayer;
    
    public void playVideo()
    {
        videoPlayer.Play();
    }
    public void pauseVideo()
    {
        videoPlayer.Pause();
    }
    public void stopVideo()
    {
        videoPlayer.Stop();
    }
    public void reStartVideo()
    {
        videoPlayer.Stop();
        videoPlayer.Play();
    }
}
