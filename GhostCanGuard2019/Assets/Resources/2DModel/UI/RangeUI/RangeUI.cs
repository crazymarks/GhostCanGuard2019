using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
[RequireComponent(typeof(Image),typeof(Animator))]
public class RangeUI : MonoBehaviour
{
    Image image;
    Animator animator;
    bool ifShown = false;
    // Start is called before the first frame update
    public void Show(float range)
    {
        if (!ifShown)
        {
            image = GetComponent<Image>();
            image.enabled = true;
            animator = GetComponent<Animator>();
            animator.enabled = true;
            image.rectTransform.offsetMax = new Vector2(range, range);
            image.rectTransform.offsetMin = new Vector2(-range, -range);
            ifShown = true;
        }
        
    }
    public void Hide()
    {
        if (ifShown)
        {
            image = GetComponent<Image>();
            image.enabled = false;
            animator = GetComponent<Animator>();
            animator.enabled = false;
            ifShown = false;
        }
        
    }
    public void SetColor(Color color)
    {
        image = GetComponent<Image>();
        image.color = color;
    }
}
