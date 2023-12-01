using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScenesManager : MonoBehaviour
{
    public static ScenesManager Singleton { get; private set; }

    private void Awake()
    {
        if (Singleton != null && Singleton != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Singleton = this;
        }
    }

    public void GoToScoreScreen()
    {
        SceneManager.LoadScene("Score");
    }

    public void GoToMainScreen()
    {
        SceneManager.LoadScene("Main");
    }
}
