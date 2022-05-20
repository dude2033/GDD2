using System.Collections.Generic;
using FMOD.Studio;
using FMODUnity;
using UnityEngine;

[RequireComponent(typeof(StudioEventEmitter))]
public class Companion : Singleton<Companion>
{
    [Header("Insults")]
    [SerializeField] private List<EventReference> insultList = new List<EventReference>();
    
    [Header("Room specific")]
    [SerializeField] private List<VoiceLineListEntry> roomVoiceLineList = new List<VoiceLineListEntry>();
    private Dictionary<string, EventReference> roomVoiceLineDictionary = new Dictionary<string, EventReference>();

    private StudioEventEmitter emitter;

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
