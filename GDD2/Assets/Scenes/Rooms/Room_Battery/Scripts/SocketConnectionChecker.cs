using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SocketConnectionChecker : MonoBehaviour
{
    public bool allPairsConnected = false;
    public UnityEvent allPairsConnectedEvent;
    private List<CableSocketPair> socketPairList = new List<CableSocketPair>();

    private void Awake()
    {
        foreach (Transform child in transform)
        {
            if (child.TryGetComponent(out CableSocketPair socketPair))
            {
                socketPair.connectionEstablishedEvent.AddListener(CheckConnections);
                socketPairList.Add(socketPair);
            }
        }
    }

    private void CheckConnections()
    {
        allPairsConnected = true;
        foreach (var socketPair in socketPairList)
        {
            if (!socketPair.isConnected)
                allPairsConnected = false;
        }

        if (allPairsConnected)
        {
            // Debug.Log("all socket pairs connected");
            allPairsConnectedEvent.Invoke();
        }
    }
}
