using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.PlayerLoop;

public enum ClientStates
{
    Shopping,
    WaitingOnLine,
    WaitingOnScale,
    Leaving,
}

public class ClientBehavior : MonoBehaviour
{

    private float _secondsToFindNewSpot = 5f;
    [SerializeField]
    private float _maxSecondsOnLine = 8f;
    [SerializeField]
    private float _currentSecondsOnLine;
    private NavMeshAgent _agent;
    private Renderer _walkablePlane;
    private Animator _anim;

    [SerializeField]
    private ClientSO _client;

    private bool _isShopping = true;
    private ClientStates _currentState = ClientStates.Shopping;

    private void Start()
    {
        _anim = GetComponent<Animator>();
        _currentSecondsOnLine = 0f;
        _maxSecondsOnLine = 8f;
        _secondsToFindNewSpot = 5f;
        _agent = GetComponent<NavMeshAgent>();
        StartCoroutine(ChangeDestinationRoutine());
        StartCoroutine(FinishShopping());
    }

    private void Update()
    {
        if (_agent.velocity.z == 0)
        {
            StopWalking();
        }
        else
        {
            StartWalking();
        }

        if (_currentState == ClientStates.WaitingOnScale)
        {
            _currentSecondsOnLine += Time.deltaTime;
        }

    }

    public void SetClientOnScaleState()
    {
        _currentState = ClientStates.WaitingOnScale;
    }

    IEnumerator FinishShopping()
    {
        yield return new WaitForSeconds(TimeToFinishShopping());
        _isShopping = false;
        _currentState = ClientStates.WaitingOnLine;
        transform.DORotate(Vector3.down, 1f);
        GameEvents.Singleton.ClientStopShopping(this);
    }

    IEnumerator ChangeDestinationRoutine()
    {
        while (_isShopping)
        {
            yield return new WaitForSeconds(_secondsToFindNewSpot);
            if (_walkablePlane != null && _isShopping)
            {
                Vector3 randomDestination = GetRandomPositionInPlane();
                _agent.SetDestination(randomDestination);
            }
        }
    }

    public void SetWalkablePlane(GameObject plane)
    {
        _walkablePlane = plane.GetComponent<Renderer>();
    }

    private Vector3 GetRandomPositionInPlane()
    {
        var plane = _walkablePlane.GetComponent<Renderer>();
        var x = Random.Range(plane.bounds.min.x, plane.bounds.max.x);
        var z = Random.Range(plane.bounds.min.z, plane.bounds.max.z);
        return new Vector3(x, transform.position.y, z);
    }

    private float TimeToFinishShopping()
    {
        return Random.Range(_secondsToFindNewSpot * 2, _secondsToFindNewSpot * 5);
    }

    public void SetDestination(Vector3 destination)
    {
        _agent.SetDestination(destination);
    }

    public bool IsWaitingOnLine()
    {
        return _currentState == ClientStates.WaitingOnLine;
    }

    public void LeaveStore(Vector3 leaveSpot)
    {
        _currentState = ClientStates.Leaving;
        _agent.SetDestination(leaveSpot);
        StartWalking();
        Destroy(gameObject, 10f);
    }

    public ClientSO GetClient()
    {
        return _client;
    }

    private void StopWalking()
    {
        _anim.SetBool("isWalking", false);
    }

    private void StartWalking()
    {
        _anim.SetBool("isWalking", true);
    }
    public bool ReachedMaxWaitTime()
    {
        if (_currentSecondsOnLine >= _maxSecondsOnLine)
        {
            return true;
        }
        return false;
    }

    public float GetClientTimeSpentOnLine()
    {
        return Mathf.Min(_maxSecondsOnLine, _currentSecondsOnLine);
    }
}
