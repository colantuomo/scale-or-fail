using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Fruit", menuName = "Fruit/Create new", order = 1)]
public class FruitSO : ScriptableObject
{
    public string Name;
    public int Code;
    public GameObject Model;
}
