using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class CodesListAnimation : MonoBehaviour
{
    [SerializeField] GameObject codesListPanel;

    public int state = 0;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && state == 0)
        {
            LeanTween.moveLocal(codesListPanel, new Vector3(0f, 0f, 0f), 0.7f).setDelay(.5f).setEase(LeanTweenType.easeOutCirc);
            state += 1;
        }
        else if (Input.GetKeyDown(KeyCode.Space) && state == 1)
        {
            LeanTween.moveLocal(codesListPanel, new Vector3(0f, -2f, 0f), 0.7f).setDelay(.5f).setEase(LeanTweenType.easeOutCirc);
            state = 0;
        }
    }
}
