using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// enum ListPanelStates
// {
//     Open,
//     Closed,
// }

public class PauseAnimation : MonoBehaviour
{
    [SerializeField] GameObject PausePanel;

    private ListPanelStates _state = ListPanelStates.Closed;

    private void Start()
    {
        PausePanel.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (_state == ListPanelStates.Closed)
            {
                Pause();
            }
            else if (_state == ListPanelStates.Open)
            {
                Continue();
            }
        }
    }

    public void Pause()
    {
        _state = ListPanelStates.Open;
        PausePanel.SetActive(true);
        Time.timeScale = 0;
    }

    public void Continue()
    {
        _state = ListPanelStates.Closed;
        PausePanel.SetActive(false);
        Time.timeScale = 1;
    }

    public void Retry()
    {
        _state = ListPanelStates.Closed;
        PausePanel.SetActive(false);
        Time.timeScale = 1;
    }

    public void ReturnToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void Quit()
    {
        Application.Quit();
    }
}
