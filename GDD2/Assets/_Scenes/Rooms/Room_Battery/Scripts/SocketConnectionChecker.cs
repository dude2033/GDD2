using System;
using System.Collections;
using System.Collections.Generic;
using NodeCanvas.Framework;
using UnityEngine;
using UnityEngine.Events;

public class SocketConnectionChecker : MonoBehaviour
{
    public bool allPairsConnected = false;
    public UnityEvent allPairsConnectedEvent;
    private List<CableSocketPair> socketPairList = new List<CableSocketPair>();
    [Header("Room FSM")]
    [SerializeField] private SignalDefinition advanceRoomFSM;

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
            DisableSocketInteractors();
            allPairsConnectedEvent.Invoke();
            advanceRoomFSM.Invoke(null, null, true);
        }
    }
    
    [ContextMenu("SolvePuzzle")]
    public void ForceSolved()
    {
        allPairsConnected = true;
        DisableSocketInteractors();
        allPairsConnectedEvent.Invoke();
        advanceRoomFSM.Invoke(null, null, true);
    }

    private void DisableSocketInteractors()
    {
        foreach (var socketPair in socketPairList)
        {
            var listA = socketPair.cableSocketA.interactablesSelected;
            if (listA.Count > 0)
            {
                Transform selectedATransform = listA[0].transform;
                selectedATransform.GetComponent<BoxCollider>().enabled = false;
            }
            
            var listB = socketPair.cableSocketA.interactablesSelected;
            if (listB.Count > 0)
            {
                Transform selectedATransform = listB[0].transform;
                selectedATransform.GetComponent<BoxCollider>().enabled = false;
            }
        }
    }
}
