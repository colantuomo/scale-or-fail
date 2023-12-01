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

public class ScoreAnimation : MonoBehaviour
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
        fields[0].text = PlayerPrefs.GetFloat("precision").ToString("0") + "%";
        fields[1].text = PlayerPrefs.GetFloat("avg").ToString("0.00 s");
        fields[2].text = PlayerPrefs.GetFloat("score").ToString("0");
        fields[3].text = PlayerPrefs.GetFloat("totalScore").ToString("0");
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
