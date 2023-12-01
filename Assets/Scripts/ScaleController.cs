using Cinemachine;
using DG.Tweening;
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
    //[SerializeField]
    private ClientBehavior _clientLeft, _clientRight;

    [SerializeField]
    private ScaleManager _scaleManagerLeft, _scaleManagerRight;
    private Transform _currentLeftFruit, _currentRightFruit;

    [SerializeField]
    private Transform _leaveSpotL, _leaveSpotR;

    private bool _isLookingToScaleLeft = true;

    [SerializeField]
    private ParticleSystem _puffFX, _fireworkFX;

    void Update()
    {
        SwitchScaleControl();
        HandleFinishShopping();
        ScaleKeyboardManager();
    }

    private void ScaleKeyboardManager()
    {
        if (_clientLeft == null && _clientRight == null) return;

        var scaleManager = _isLookingToScaleLeft ? _scaleManagerLeft : _scaleManagerRight;

        HandleScaleNumericKeyboard(scaleManager);
        HandleScaleKeyboard(scaleManager);

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
        if (_clientRight != null)
        {
            if (_clientRight.ReachedMaxWaitTime())
            {
                HandleRightClientLeavingLine();
            }
        }
        if (_clientLeft != null)
        {
            if (_clientLeft.ReachedMaxWaitTime())
            {
                HandleLeftClientLeavingLine();
            }
        }
        if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter))
        {
            if (_clientLeft == null && _clientRight == null)
            {
                return;
            }

            if (_isLookingToScaleLeft && _clientLeft != null)
            {
                HandleLeftClientLeavingLine();
            }
            else if (!_isLookingToScaleLeft && _clientRight != null)
            {
                HandleRightClientLeavingLine();
            }
        }
    }

    private void HandleLeftClientLeavingLine()
    {
        GameEvents.Singleton.UpdateClientsLines(true);
        if (TypedCodeEqualsFruitCode(_scaleManagerLeft.GetScaleCode(), _clientLeft.GetClient().Fruit.Code))
        {
            _scaleManagerLeft.PrintFeedback("success");
            GameEvents.Singleton.UpdateLevelScore(_clientLeft.GetClientTimeSpentOnLine());
            GameEvents.Singleton.UpdateLevelStatistics(_clientLeft.GetClientTimeSpentOnLine(), true);
        }
        else
        {
            _scaleManagerLeft.PrintFeedback("failed");
            GameEvents.Singleton.UpdateLevelStatistics(_clientLeft.GetClientTimeSpentOnLine(), false);
        }
        _clientLeft.LeaveStore(_leaveSpotL.position);
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

    private void HandleRightClientLeavingLine()
    {
        GameEvents.Singleton.UpdateClientsLines(false);
        if (TypedCodeEqualsFruitCode(_scaleManagerRight.GetScaleCode(), _clientRight.GetClient().Fruit.Code))
        {
            _scaleManagerRight.PrintFeedback("success");
            GameEvents.Singleton.UpdateLevelScore(_clientRight.GetClientTimeSpentOnLine());
            GameEvents.Singleton.UpdateLevelStatistics(_clientRight.GetClientTimeSpentOnLine(), true);
        }
        else
        {
            _scaleManagerRight.PrintFeedback("failed");
            GameEvents.Singleton.UpdateLevelStatistics(_clientRight.GetClientTimeSpentOnLine(), false);
            GameEvents.Singleton.FailCodeTyping();
        }
        _clientRight.LeaveStore(_leaveSpotR.position);
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
        InstantiateLeftFruit(client.GetClient().Fruit);
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
        InstantiateRightFruit(client.GetClient().Fruit);
        _clientRight = client;
    }
    private void InstantiateLeftFruit(FruitSO fruit)
    {
        _currentLeftFruit = _scaleManagerLeft.PutFruitOnScaleSpot(fruit);
        LeanTween.scale(_currentLeftFruit.gameObject, _currentLeftFruit.localScale * 2f, 0.5f).setEaseShake();
        Instantiate(_puffFX, _currentLeftFruit.position, Quaternion.identity);
    }

    private void InstantiateRightFruit(FruitSO fruit)
    {
        _currentRightFruit = _scaleManagerRight.PutFruitOnScaleSpot(fruit);
        LeanTween.scale(_currentRightFruit.gameObject, _currentRightFruit.localScale * 2f, 0.5f).setEaseShake();
        Instantiate(_puffFX, _currentRightFruit.position, Quaternion.identity);
    }

    private bool TypedCodeEqualsFruitCode(string typedCode, int fruitCode)
    {
        int integerCode = 0;
        int.TryParse(typedCode, out integerCode);
        if (integerCode == fruitCode)
        {
            return true;
        }
        return false;
    }
}
