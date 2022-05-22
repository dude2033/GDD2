using System;
using System.Collections.Generic;
using FMOD.Studio;
using FMODUnity;
using NodeCanvas.Framework;
using UnityEngine;
using Random = UnityEngine.Random;

[RequireComponent(typeof(StudioEventEmitter))]
public class Companion : Singleton<Companion>
{
    [Header("Insults")]
    [SerializeField] private List<EventReference> insultList = new List<EventReference>();
    
    [Header("Room specific")]
    [SerializeField] private List<VoiceLineListEntry> roomVoiceLineList = new List<VoiceLineListEntry>();
    private Dictionary<string, EventReference> roomVoiceLineDictionary = new Dictionary<string, EventReference>();

    [Header("FSM Signal Emitter")] 
    [SerializeField] private SignalDefinition hintSignalDefinition;

    public StudioEventEmitter emitter;

    private void Start()
    {
        // disable the mesh
        // the companion must be explicitly activated
        transform.GetChild(0).gameObject.SetActive(false);
    }

    public void ActivateCompanion()
    {
        transform.GetChild(0).gameObject.SetActive(true);
    }

    public void LoadRoomVoiceLineList(List<VoiceLineListEntry> newVoiceLineList)
    {
        roomVoiceLineList = newVoiceLineList;
        GenerateVoiceLineDictionary();
    }

    [ContextMenu("generate voice line dictionary")]
    private void GenerateVoiceLineDictionary()
    {
        roomVoiceLineDictionary = new Dictionary<string, EventReference>();
        foreach (VoiceLineListEntry voiceLineEntry in roomVoiceLineList)
        {
            roomVoiceLineDictionary.Add(voiceLineEntry.voiceLineID, voiceLineEntry.voiceLineReference);
        }
    }
    
    [ContextMenu("Random insult")]
    public void TriggerRandomInsult()
    {
        emitter.ChangeEvent(insultList[Random.Range(0, insultList.Count)]);
        emitter.Play();
    }
    
    [ContextMenu("Trigger voice line test")]
    public void TriggerTestVoiceLine()
    {
        TriggerVoiceLine("test");
    }

    public void TriggerVoiceLine(string voiceLineID)
    {
        if (roomVoiceLineDictionary.ContainsKey(voiceLineID))
        {
            emitter.ChangeEvent(roomVoiceLineDictionary[voiceLineID]);
            emitter.Play();
        }
        else
        {
            Debug.LogError("voice line dictionary does not contain key: " + voiceLineID);
        }
    }
    
    public bool CheckIfPlaying() 
    {
        return emitter.IsPlaying();
    }
    
    // triggers the next available hint by consulting the room FSM
    [ContextMenu("Trigger hint")]
    public void TriggerHint()
    {
        hintSignalDefinition.Invoke(null, null, true);
    }

    public void ChangeVoiceLines(List<VoiceLineListEntry> newVoiceLines)
    {
        roomVoiceLineList = newVoiceLines;
        GenerateVoiceLineDictionary();
    }
    
    protected override void OnApplicationQuitCallback()
    {
    }

    protected override void OnEnableCallback()
    {
        emitter = GetComponent<StudioEventEmitter>();
    }
}

[System.Serializable]
public class VoiceLineListEntry
{
    public string voiceLineID = "";
    public EventReference voiceLineReference;
}
