using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeAnimation : MonoBehaviour
{
    public CanvasGroup canvasGroup;
    private bool fadeIn = false;
    private bool fadeOut = false;
    public float TimeToFade;

    void Update()
    {
        if(fadeIn == true)
        {
            fadeInAnimation();
        }
        if(fadeOut == true)
        {
            fadeOutAnimation();
        }
    }

    private void fadeInAnimation()
    {
        if(canvasGroup.alpha < 1)
        {
            canvasGroup.alpha += (1/TimeToFade) * Time.deltaTime;
            if(canvasGroup.alpha >= 1)
            {
                fadeIn = false;
            }
        }
    }

    private void fadeOutAnimation()
    {
        if(canvasGroup.alpha >= 0)
        {
            canvasGroup.alpha -= (1/TimeToFade) * Time.deltaTime;
            if(canvasGroup.alpha == 0)
            {
                fadeIn = false;
            }
        }
    }

    public void FadeIn()
    {
        fadeIn = true;
    }

    public void FadeOut()
    {
        fadeOut = true;
    }
}
