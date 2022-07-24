using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit.Inputs;

public class RoomSetup : MonoBehaviour
{
    [SerializeField] private List<GameObject> destroyOnSetup = new List<GameObject>();
    [SerializeField] private List<VoiceLineListEntry> voiceLineList = new List<VoiceLineListEntry>();
    [SerializeField] private InputActionManager inputActionManager;
    public bool setupOnStart = false;
    public bool destroyObjects = true;

    private void Start()
    {
        if (setupOnStart)
        {
            SetupRoom();
        }

        // the toggle is needed to register the objects
        // inputActionManager.enabled = false;
        // inputActionManager.enabled = true;
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
