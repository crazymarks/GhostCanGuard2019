using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThiefAnimationController : MonoBehaviour
{
    [SerializeField]
    Thief tf;
    Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        //if(tf.thiefState==Thief.ThiefState.PAUSE|| tf.thiefState == Thief.ThiefState.END||tf.thiefState == Thief.ThiefState.EXITED)
        //{
        //    animator.SetBool("Wait", true);
        //}
    }
    public void setWaitAnimation()
    {
        animator.SetBool("Wait", true);
    }
    public void setRunAnimation()
    {
        animator.SetBool("Wait", false);
    }
}
