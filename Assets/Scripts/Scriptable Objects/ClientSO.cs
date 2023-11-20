using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Client", menuName = "Client/Create new", order = 1)]
public class ClientSO : ScriptableObject
{
    public string Name;
    public FruitSO Fruit;
    public GameObject Model;
}
