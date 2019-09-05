using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowToturialImage : MonoBehaviour
{
    public Image image;
    bool HasShown = false;
    // Start is called before the first frame update
    void Start()
    {
        HasShown = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (StopSystem.Instance.stopped && !HasShown)
        {
            Show();
        }
        if (Input.GetButtonDown("Cancel") && HasShown)
        {
            Hide();
        }
            
    }
    void Show()
    {
        StopSystem.Instance.gamestop(StopSystem.PauseState.SystemPause);
        StopSystem.Instance.clearselectobj();
        image.enabled = true;
        HasShown = true;
    }
    void Hide()
    {
        StopSystem.Instance.gamestop(StopSystem.PauseState.Resume);
        image.enabled = false;
    }
}
