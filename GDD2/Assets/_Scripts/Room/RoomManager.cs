using System;
using NodeCanvas.Framework;
using UnityEngine;

public class RoomManager : MonoBehaviour
{
    [SerializeField] private SignalDefinition advanceRoomSignalDefinition;
    private Animator proceed;
    
    [ContextMenu("Proceed to next room")]
    public void ProceedToNextRoom()
    {
        SceneProgressionManager.Instance.Progress();
    }
    
    [ContextMenu("advance room fsm")]
    public void AdvanceRoomFSM()
    {
        advanceRoomSignalDefinition.Invoke(null, null, true);
    }
    
    public void PlayCompanionVoiceLine(string voiceLineID)
    {
        Companion.Instance.TriggerVoiceLine(voiceLineID);
    }
}
