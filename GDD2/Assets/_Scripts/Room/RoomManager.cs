using System;
using NodeCanvas.Framework;
using UnityEngine;

public class RoomManager : MonoBehaviour
{
    [SerializeField] private SignalDefinition advanceRoomSignalDefinition;
    private Animator proceed;

    // Room FSM functions
    #region Room FSM
    [ContextMenu("Proceed to next room")]
    public void ProceedToNextRoom()
    {
        if(SceneProgressionManager.Instance)
            SceneProgressionManager.Instance.Progress();
        else
            Debug.LogWarning("there is no scene manager - did you load the scene from the main scene?");
    }
    
    [ContextMenu("Advance room FSM")]
    public void AdvanceRoomFSM() => advanceRoomSignalDefinition.Invoke(null, null, true);
    
    #endregion

    // Companion specific function
    #region Companion functions
    public void PlayCompanionVoiceLine(string voiceLineID) => Companion.Instance.TriggerVoiceLine(voiceLineID);
    public void SetCompanionParticleFX(bool state) => Companion.Instance.SetParticleFX(state);

    #endregion
}
