using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuAnimation : MonoBehaviour
{
    private GameObject menuTitle;
    private GameObject menuButtons;

    void Start()
    {
        StartCoroutine(_MainMenuAnimation());
    }

    private IEnumerator _MainMenuAnimation()
    {
        if (menuTitle == null)
        {
            menuTitle = GameObject.Find("GameTitleImage");
        }
        if (menuButtons == null)
        {
            menuButtons = GameObject.Find("MenuButtons");
        }

        menuTitle.GetComponent<FadeAnimation>().FadeIn();
        yield return new WaitForSeconds(menuTitle.GetComponent<FadeAnimation>().TimeToFade);

        menuButtons.GetComponent<FadeAnimation>().FadeIn();
        yield return new WaitForSeconds(menuButtons.GetComponent<FadeAnimation>().TimeToFade);
    }
}
