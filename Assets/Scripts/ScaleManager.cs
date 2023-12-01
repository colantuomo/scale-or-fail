using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ScaleManager : MonoBehaviour
{
    [SerializeField]
    private TMP_Text _keyboardScreen, _feedbackScreenText;
    [SerializeField]
    private CanvasRenderer _feedbackScreen;
    [SerializeField]
    private int _maxDigits = 5;
    [SerializeField]
    private Transform _fruitSpot;


    public void AddDigitToKeyboardScreen(string digit)
    {
        if (_keyboardScreen.text.Length < _maxDigits)
        {
            _keyboardScreen.text += digit;
        }
    }

    public void Clear()
    {
        _keyboardScreen.text = "";
    }

    public void RemoveLastDigit()
    {
        if (_keyboardScreen.text.Length == 0) return;

        _keyboardScreen.text = _keyboardScreen.text.Remove(_keyboardScreen.text.Length - 1, 1);
    }

    public string GetScaleCode()
    {
        return _keyboardScreen.text;
    }

    public Transform PutFruitOnScaleSpot(FruitSO fruit)
    {
        return Instantiate(fruit.Model, _fruitSpot.position, Quaternion.identity).transform;
    }

    public void PrintFeedback(string feedbackType)
    {
        if (feedbackType == "success")
        {
            _feedbackScreen.GetComponent<Image>().color = Color.green;
            _feedbackScreenText.text = "Correct!";
        }
        else if (feedbackType == "failed")
        {
            _feedbackScreen.GetComponent<Image>().color = Color.red;
            _feedbackScreenText.text = "Incorrect!";
        }
    }

}
