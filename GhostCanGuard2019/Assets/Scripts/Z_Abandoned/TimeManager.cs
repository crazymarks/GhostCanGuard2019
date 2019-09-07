using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeManager
{
    //bool pauseflag = false;

    TimeManager(){
        timescale.Add(1f);
    }


    private List<float> timescale = new List<float>();

    private void addTimescale(float scale)
    {
        timescale.Add(scale);
    }
    private void removeTimescale()
    {
        if (timescale.Count > 1)
            timescale.RemoveAt(timescale.Count - 1);
        else
            Debug.Log("Warning:TimeScale List has only one value");
    }

    private void fetchLastTimeScale()
    {
        Time.timeScale = timescale[timescale.Count - 1];
    }
    public void pause()
    {
        addTimescale(0f);
        fetchLastTimeScale();
    }
    


}
