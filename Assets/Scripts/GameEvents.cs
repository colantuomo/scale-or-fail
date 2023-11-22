using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEvents : MonoBehaviour
{
    public static GameEvents Singleton { get; private set; }

    private void Awake()
    {
        if (Singleton != null && Singleton != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Singleton = this;
        }
    }

    public event Action<ClientBehavior> OnClientStopShopping;
    public void ClientStopShopping(ClientBehavior client)
    {
        OnClientStopShopping?.Invoke(client);
    }


    public event Action<bool> OnUpdateClientsLines;
    public void UpdateClientsLines(bool isLeft)
    {
        OnUpdateClientsLines?.Invoke(isLeft);
    }

    public event Action<ClientSO, string>OnUpdateLevelScore;
    public void UpdateLevelScore(ClientSO client, string text)
    {
        OnUpdateLevelScore?.Invoke(client, text);
    }
}
