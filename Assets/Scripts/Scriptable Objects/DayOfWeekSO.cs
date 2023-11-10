using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DayOfWeek", menuName = "DayOfWeek/Create new", order = 1)]

public class DayOfWeekSO : ScriptableObject
{
    public List<ClientSO> Clients;
}
