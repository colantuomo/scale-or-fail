using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum ListPanelStates
{
    Open,
    Closed,
}

public class CodesListAnimation : MonoBehaviour
{
    [SerializeField] GameObject codesListPanel;

    private ListPanelStates _state = ListPanelStates.Closed;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && _state == ListPanelStates.Closed)
        {
            LeanTween.moveLocal(codesListPanel, new Vector3(0f, 0f, 0f), 0.7f).setDelay(.5f).setEase(LeanTweenType.easeOutCirc);
            _state = ListPanelStates.Open;
        }
        else if (Input.GetKeyDown(KeyCode.Space) && _state == ListPanelStates.Open)
        {
            LeanTween.moveLocal(codesListPanel, new Vector3(0f, -2f, 0f), 0.7f).setDelay(.5f).setEase(LeanTweenType.easeOutCirc);
            _state = ListPanelStates.Closed;
        }
    }
}
