using UnityEngine;

public class ShowTutorialImage : MonoBehaviour
{
    public enum whenToShow
    {
        gamestart,
        firstStop
    }
    public Sprite Bracket;
    public Sprite Desc;
    public whenToShow wts;
    public DesceriptionAnimation anim;
    bool HasShown = false;
    // Start is called before the first frame update
    void Start()
    {
        HasShown = false;
        anim.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(wts == whenToShow.firstStop)
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
        else
        {
            if(GameManager.Instance.IfGameStart && !HasShown)
            {
                Show();
            }
            if (Input.GetButtonDown("Cancel") && HasShown)
            {
                Hide();
            }
        }
            
    }
    void Show()
    {
        StopSystem.Instance.gamestop(StopSystem.PauseState.DescriptionOpen);
        StopSystem.Instance.clearselectobj();
        anim.gameObject.SetActive(true);
        anim.generate(Bracket,Desc);
        anim.reset();
        anim.trigger();
        HasShown = true;
    }
    void Hide()
    {
        anim.reset();
        anim.gameObject.SetActive(false);
        StopSystem.Instance.gamestop(StopSystem.PauseState.DescriptionClose);
        if(wts == whenToShow.gamestart)
            StopSystem.Instance.gamestop(StopSystem.PauseState.Normal);
        Destroy(gameObject);
    }
    
}
