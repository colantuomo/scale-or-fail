using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScaleManager : MonoBehaviour
{
    [SerializeField]
    private TMP_Text _keyboardScreen;
    [SerializeField]
    private int _maxDigits = 5;


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

}
