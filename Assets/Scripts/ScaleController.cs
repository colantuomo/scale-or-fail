using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaleController : MonoBehaviour
{
    [SerializeField]
    private CinemachineVirtualCamera _mainCam;

    [SerializeField]
    private Transform _scaleLeft, _scaleRight, _centralPoint;
    private ClientBehavior _clientLeft, _clientRight;

    [SerializeField]
    private Transform _leaveSpot;

    private bool _isLookingToScaleLeft = true;
    void Update()
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
                _mainCam.LookAt = _scaleLeft;
            }
            else
            {
                _mainCam.LookAt = _scaleRight;
            }
        }
        if (Input.GetKeyDown(KeyCode.Return))
        {
            print($"Enter Pressed - _isLookingToScaleLeft: {_isLookingToScaleLeft}");
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

                if (_clientRight == null)
                {
                    _mainCam.LookAt = _centralPoint;
                }
                else
                {
                    _mainCam.LookAt = _scaleRight;
                }
            }
            else if(!_isLookingToScaleLeft && _clientRight != null)
            {
                GameEvents.Singleton.UpdateClientsLines(false);
                _clientRight.LeaveStore(_leaveSpot.position);
                _clientRight = null;
                _isLookingToScaleLeft = true;

                if (_clientLeft == null)
                {
                    _mainCam.LookAt = _centralPoint;
                }
                else
                {
                    _mainCam.LookAt = _scaleLeft;
                }
            }
        }
    }



    public void SetClientLeft(ClientBehavior client)
    {
        if (_clientLeft != null) return;
        _isLookingToScaleLeft = true;
        _mainCam.LookAt = _scaleLeft;
        _clientLeft = client;
    }

    public void SetClientRight(ClientBehavior client)
    {
        if (_clientRight != null) return;
        _isLookingToScaleLeft = false;
        _mainCam.LookAt = _scaleRight;
        _clientRight = client;
    }
}
