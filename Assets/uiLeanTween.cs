using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[System.Serializable]
public struct TextField
{
    public TextMeshProUGUI textBox;
    public string text;
}

public class uiLeanTween : MonoBehaviour
{
    [SerializeField] GameObject scorePanel;
    public TextField[] fields;
    private float textSpeed = 0.1f;
    private float textPause = 0.5f;

    void Start()
    {
        foreach (TextField field in fields)
        {
            field.textBox.text = string.Empty;
        }

        LeanTween.moveLocal(scorePanel, new Vector3(0f, 0f, 0f), 0.7f).setDelay(.5f).setEase(LeanTweenType.easeOutCirc).setOnComplete(ShowScore);
    }

    void ShowScore()
    {
        StartCoroutine(TypeLine());
    }

    IEnumerator TypeLine()
    {
        foreach (TextField field in fields)
        {
            foreach (char c in field.text.ToCharArray())
            {
                field.textBox.text += c;
                yield return new WaitForSeconds(textSpeed);
            }

            yield return new WaitForSeconds(textPause);
        }
    }
}
