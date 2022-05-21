using System;
using System.Collections.Generic;
using UnityEngine;

public class RoomSetup : MonoBehaviour
{
    [SerializeField] private List<GameObject> destroyOnSetup = new List<GameObject>();
    [SerializeField] private List<VoiceLineListEntry> voiceLineList = new List<VoiceLineListEntry>();
    public bool setupOnStart = false;
    public bool destroyObjects = true;

    private void Start()
    {
        if (setupOnStart)
        {
            SetupRoom();
        }
    }

    [ContextMenu("Setup Room")]
    public void SetupRoom()
    {
        if (destroyObjects)
        {
            // destroy additional XR rigs
            foreach (var go in destroyOnSetup)
            {
                Destroy(go);
            }
        }
        
        LoadVoiceLines();
    }

    private void LoadVoiceLines()
    {
        Companion.Instance.ChangeVoiceLines(voiceLineList);
    }
}
