using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineSpotBehavior : MonoBehaviour
{
    public event Action<ClientBehavior> OnClientIsOnScale;
    public void ClientIsOnScale(ClientBehavior client)
    {
        OnClientIsOnScale?.Invoke(client);
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.transform.TryGetComponent(out ClientBehavior client))
        {
            if (client.IsWaitingOnLine())
            {
                ClientIsOnScale(client);
            }
        }
    }
}
