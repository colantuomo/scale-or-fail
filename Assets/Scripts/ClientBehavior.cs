using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public enum ClientStates
{
    Shopping,
    WaitingOnLine,
    Leaving,
}

public class ClientBehavior : MonoBehaviour
{
    [SerializeField]
    private float _secondsToFindNewSpot = 5f;
    private NavMeshAgent _agent;
    private Renderer _walkablePlane;

    private bool _isShopping = true;
    private ClientStates _currentState = ClientStates.Shopping;

    private void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
        StartCoroutine(ChangeDestinationRoutine());
        StartCoroutine(FinishShopping());
    }

    IEnumerator FinishShopping()
    {
        yield return new WaitForSeconds(TimeToFinishShopping());
        _isShopping = false;
        _currentState = ClientStates.WaitingOnLine;
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
        var x = Random.Range(-_walkablePlane.bounds.size.x, _walkablePlane.bounds.size.x);
        var z = Random.Range(-_walkablePlane.bounds.size.z, _walkablePlane.bounds.size.z);
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
        Destroy(gameObject, 10f);
    }
}