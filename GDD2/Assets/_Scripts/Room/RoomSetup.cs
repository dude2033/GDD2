using System.Collections.Generic;
using UnityEngine;

public class RoomSetup : MonoBehaviour
{
    [SerializeField] private List<GameObject> destroyOnSetup = new List<GameObject>();

    [ContextMenu("Setup Room")]
    public void SetupRoom()
    {
        // destroy additional XR rigs
        foreach (var go in destroyOnSetup)
        {
            Destroy(go);
        }
    }
}
