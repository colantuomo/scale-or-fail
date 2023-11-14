using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScaleController : MonoBehaviour
{
    [SerializeField]
    private CinemachineVirtualCamera _mainCam;

    [SerializeField]
    private Transform _centralPoint;
    private ClientBehavior _clientLeft, _clientRight;

    [SerializeField]
    private ScaleManager _scaleManagerLeft, _scaleManagerRight;

    [SerializeField]
    private Transform _leaveSpot;

    private bool _isLookingToScaleLeft = true;

    [SerializeField]
    private bool _hasNumericKeyboard = true;

    void Update()
    {
        SwitchScaleControl();
        HandleFinishShopping();
        ScaleKeyboardManager();
    }

    private void ScaleKeyboardManager()
    {
        if (_clientLeft == null || _clientRight == null) return;

        var scaleManager = _isLookingToScaleLeft ? _scaleManagerLeft : _scaleManagerRight;

        if (_hasNumericKeyboard)
        {
            HandleScaleNumericKeyboard(scaleManager);
        }
        else
        {
            HandleScaleKeyboard(scaleManager);
        }

        //if (Input.GetKeyDown(KeyCode.Space)) scaleManager.Clear();
        if (Input.GetKeyDown(KeyCode.Backspace)) scaleManager.RemoveLastDigit();
    }

    private void HandleScaleNumericKeyboard(ScaleManager scaleManager)
    {
        if (Input.GetKeyDown(KeyCode.Keypad9)) scaleManager.AddDigitToKeyboardScreen("9");
        if (Input.GetKeyDown(KeyCode.Keypad8)) scaleManager.AddDigitToKeyboardScreen("8");
        if (Input.GetKeyDown(KeyCode.Keypad7)) scaleManager.AddDigitToKeyboardScreen("7");

        if (Input.GetKeyDown(KeyCode.Keypad6)) scaleManager.AddDigitToKeyboardScreen("6");
        if (Input.GetKeyDown(KeyCode.Keypad5)) scaleManager.AddDigitToKeyboardScreen("5");
        if (Input.GetKeyDown(KeyCode.Keypad4)) scaleManager.AddDigitToKeyboardScreen("4");

        if (Input.GetKeyDown(KeyCode.Keypad3)) scaleManager.AddDigitToKeyboardScreen("3");
        if (Input.GetKeyDown(KeyCode.Keypad2)) scaleManager.AddDigitToKeyboardScreen("2");
        if (Input.GetKeyDown(KeyCode.Keypad1)) scaleManager.AddDigitToKeyboardScreen("1");
        if (Input.GetKeyDown(KeyCode.Keypad0)) scaleManager.AddDigitToKeyboardScreen("0");
    }

    private void HandleScaleKeyboard(ScaleManager scaleManager)
    {
        if (Input.GetKeyDown(KeyCode.Q)) scaleManager.AddDigitToKeyboardScreen("9");
        if (Input.GetKeyDown(KeyCode.W)) scaleManager.AddDigitToKeyboardScreen("8");
        if (Input.GetKeyDown(KeyCode.E)) scaleManager.AddDigitToKeyboardScreen("7");

        if (Input.GetKeyDown(KeyCode.D)) scaleManager.AddDigitToKeyboardScreen("6");
        if (Input.GetKeyDown(KeyCode.S)) scaleManager.AddDigitToKeyboardScreen("5");
        if (Input.GetKeyDown(KeyCode.A)) scaleManager.AddDigitToKeyboardScreen("4");

        if (Input.GetKeyDown(KeyCode.C)) scaleManager.AddDigitToKeyboardScreen("3");
        if (Input.GetKeyDown(KeyCode.X)) scaleManager.AddDigitToKeyboardScreen("2");
        if (Input.GetKeyDown(KeyCode.Z)) scaleManager.AddDigitToKeyboardScreen("1");
        if (Input.GetKeyDown(KeyCode.LeftAlt)) scaleManager.AddDigitToKeyboardScreen("0");
    }

    private void HandleFinishShopping()
    {
        if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter))
        {
            if (_clientLeft == null && _clientRight == null)
            {
                return;
            }

            if (_isLookingToScaleLeft && _clientLeft != null)
            {
                GameEvents.Singleton.UpdateClientsLines(true);
                _clientLeft.LeaveStore(_leaveSpot.position);
                _clientLeft = null;
                _isLookingToScaleLeft = false;
                _scaleManagerLeft.Clear();

                if (_clientRight == null)
                {
                    _mainCam.LookAt = _centralPoint;
                }
                else
                {
                    _mainCam.LookAt = _scaleManagerRight.transform;
                }
            }
            else if (!_isLookingToScaleLeft && _clientRight != null)
            {
                GameEvents.Singleton.UpdateClientsLines(false);
                _clientRight.LeaveStore(_leaveSpot.position);
                _clientRight = null;
                _isLookingToScaleLeft = true;
                _scaleManagerRight.Clear();

                if (_clientLeft == null)
                {
                    _mainCam.LookAt = _centralPoint;
                }
                else
                {
                    _mainCam.LookAt = _scaleManagerLeft.transform;
                }
            }
        }
    }

    private void SwitchScaleControl()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if (_clientLeft == null || _clientRight == null)
            {
                return;
            }

            _isLookingToScaleLeft = !_isLookingToScaleLeft;

            if (_isLookingToScaleLeft)
            {
                _mainCam.LookAt = _scaleManagerLeft.transform;
            }
            else
            {
                _mainCam.LookAt = _scaleManagerRight.transform;
            }
        }
    }



    public void SetClientLeft(ClientBehavior client)
    {
        if (_clientLeft != null) return;

        if (_clientRight == null)
        {
            _isLookingToScaleLeft = true;
            _mainCam.LookAt = _scaleManagerLeft.transform;
        }

        _clientLeft = client;
    }

    public void SetClientRight(ClientBehavior client)
    {
        if (_clientRight != null) return;

        if (_clientLeft == null)
        {
            _isLookingToScaleLeft = false;
            _mainCam.LookAt = _scaleManagerRight.transform;
        };

        _clientRight = client;
    }
}
