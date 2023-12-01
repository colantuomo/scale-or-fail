using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lifebar : MonoBehaviour
{
    [SerializeField]
    private Transform _lifeIcon;
    [SerializeField]
    private int _lifeCount = 3;
    private int _currentLife = 0;

    private List<Transform> _lifeBars = new List<Transform>();

    private void Start()
    {
        _currentLife = _lifeCount;
        for (var i = 0; i < _lifeCount; i++)
        {
            var life = Instantiate(_lifeIcon);
            life.transform.SetParent(transform, false);
            _lifeBars.Add(life);
        }
    }

    public void DecreaseByOne()
    {
        var idx = _lifeBars.Count - 1;
        var lifebar = _lifeBars[idx];
        _lifeBars.RemoveAt(idx);
        Destroy(lifebar.gameObject);
        if (_lifeBars.Count == 0)
        {
            GameEvents.Singleton.PlayerHasLost();
        }
    }
}
