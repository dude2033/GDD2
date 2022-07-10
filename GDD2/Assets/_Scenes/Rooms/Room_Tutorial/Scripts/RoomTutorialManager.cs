using System.Collections;
using NodeCanvas.StateMachines;
using UnityEngine;
using UnityEngine.Timeline;

public class RoomTutorialManager : RoomManager
{
    private bool lookedAtPlusZ = false;
    
    private void Start()
    {
        Companion.Instance.ActivateCompanion();
    }
    
    // signals are handled by the timeline
    public void SignalPlayIntroVoiceLine()
    {
        AdvanceRoomFSM();
    }

    public void LookedAtPlusZ()
    {
    }
    
}
