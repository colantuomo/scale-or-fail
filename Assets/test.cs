using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class test : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI textTest;

    public string[] lines;
    public float textSpeed;
    private int index;

    void Start()
    {
        textTest.text = string.Empty;
        ShowScore();
    }

    void ShowScore()
    {
        index = 0;
        StartCoroutine(TypeLine());
    }

    IEnumerator TypeLine()
    {
        foreach (char c in lines[index].ToCharArray())
        {
            textTest.text += c;
            yield return new WaitForSeconds(textSpeed);
        }
    }
}
