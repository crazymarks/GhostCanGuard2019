using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class AudioController 
{

    public static Dictionary<string, AudioClip> audioDic = new Dictionary<string, AudioClip>();
    // Start is called before the first frame update
    //<param name ="dir"> SE路径, Resources</param>
    //<param name ="name">SEの名前</param>
    public static void PlaySnd(string dir, string name)
    {
        AudioClip clip = LoadClip(dir, name);
        if (clip != null)

            AudioSource.PlayClipAtPoint(clip, Vector3.zero); //音楽プレイする場所

        else

            Debug.LogError("Clip is Missing" + name);
    }
        

    public static AudioClip LoadClip(string dir, string name)
        {
            if (!audioDic.ContainsKey(name))
            {
                string dirSound = dir + "/" + name;
                AudioClip clip = Resources.Load(dirSound) as AudioClip;
                if (clip != null)
                    audioDic.Add(clip.name, clip);
            }
            return audioDic[name];
        }

    

       
 }
    

