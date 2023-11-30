using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayOfWeekManager : MonoBehaviour
{
    [SerializeField]
    private DayOfWeekSO _currentDay;
    [SerializeField]
    private GameObject _walkablePlane;

    [SerializeField]
    private ScaleController _scaleController;
    [SerializeField]
    private Transform _leftClientLine, _rightClientLine;
    [SerializeField]
    private LineSpotBehavior _leftSpot, _rightSpot;
    [SerializeField]
    private Transform _clientSpawnSpot;
    [SerializeField]
    private float _lineOffset = 10f;

    private List<ClientBehavior> _clientsInLeftLine = new();
    private List<ClientBehavior> _clientsInRightLine = new();

    private void Start()
    {
        InstantiateClients();
        SubscribeEvents();
    }

    private void SubscribeEvents()
    {
        GameEvents.Singleton.OnClientStopShopping += OnClientStopShopping;
        GameEvents.Singleton.OnUpdateClientsLines += OnUpdateClientsLines;

        _leftSpot.OnClientIsOnScale += LeftSpotOnClientIsOnScale;
        _rightSpot.OnClientIsOnScale += RightSpotOnClientIsOnScale;
    }

    private void OnUpdateClientsLines(bool isLeft)
    {
        if (isLeft)
        {
            AdjustLeftLine();
        }
        else
        {
            AdjustRightLine();
        }
    }

    private void LeftSpotOnClientIsOnScale(ClientBehavior clientBehavior)
    {
        _scaleController.SetClientLeft(clientBehavior);
        _clientsInLeftLine.Remove(clientBehavior);
    }

    private void RightSpotOnClientIsOnScale(ClientBehavior clientBehavior)
    {
        _scaleController.SetClientRight(clientBehavior);
        _clientsInRightLine.Remove(clientBehavior);
    }

    private void OnClientStopShopping(ClientBehavior clientBehavior)
    {
        if (UseLeftLine())
        {
            _clientsInLeftLine.Add(clientBehavior);
        }
        else
        {
            _clientsInRightLine.Add(clientBehavior);
        }
        AdjustLeftLine();
        AdjustRightLine();
    }

    private void AdjustLeftLine()
    {
        var destination = _leftClientLine.position;
        _clientsInLeftLine.ForEach((client) =>
        {
            client.SetDestination(destination);
            destination = new Vector3(destination.x, destination.y, destination.z + _lineOffset);
        });
    }

    private void AdjustRightLine()
    {
        var destination = _rightClientLine.position;
        _clientsInRightLine.ForEach((client) =>
        {
            client.SetDestination(destination);
            destination = new Vector3(destination.x, destination.y, destination.z + _lineOffset);
        });
    }

    private void InstantiateClients()
    {
        var spot = _clientSpawnSpot.position;
        _currentDay.Clients.ForEach((client) =>
        {
            var cli = Instantiate(client.Model, spot, client.Model.transform.rotation);
            if (cli.TryGetComponent(out ClientBehavior clientBehavior))
            {
                clientBehavior.SetWalkablePlane(_walkablePlane);
            }
            spot = new Vector3(spot.x + 5f, spot.y, spot.z);
        });

    }

    private bool UseLeftLine()
    {
        var max = 5;
        var rnd = Random.Range(0, max);
        return rnd < max / 2;
    }

    private Vector3 GetRandomPositionInPlane()
    {
        var plane = _walkablePlane.GetComponent<Renderer>();
        var x = Random.Range(plane.bounds.min.x, plane.bounds.max.x);
        var z = Random.Range(plane.bounds.min.z, plane.bounds.max.z);
        return new Vector3(x, transform.position.y, z);
    }

}
