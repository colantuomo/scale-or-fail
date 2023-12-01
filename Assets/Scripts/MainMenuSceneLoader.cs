using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuSceneLoader : MonoBehaviour
{
    private GameObject fadeOutObject;

    private IEnumerator _LoadMainGameplaySceneAnimation()
    {
        fadeOutObject = GameObject.Find("FadeOutObject");

        fadeOutObject.GetComponent<FadeAnimation>().FadeIn();
        yield return new WaitForSeconds(fadeOutObject.GetComponent<FadeAnimation>().TimeToFade);

        SceneManager.LoadScene("Main");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
