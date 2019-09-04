using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowTorurialImage : MonoBehaviour
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
        if (stop.Instance.stopped)
        {
            Show();
        }
        else Hide();
        if (Input.GetButtonDown("Cancel"))
        {
            stop.Instance.gamestop(stop.PauseState.ObserverMode);
            Hide();
        }
            
    }
    void Show()
    {
        if (!HasShown)
        {
            stop.Instance.gamestop(stop.PauseState.DescriptionOpen);
            image.enabled = true;
            HasShown = true;
        }
    }
    void Hide()
    {
        if (HasShown)
        {
            image.enabled = false;
            stop.Instance.gamestop(stop.PauseState.DescriptionClose);
        }
    }
}
