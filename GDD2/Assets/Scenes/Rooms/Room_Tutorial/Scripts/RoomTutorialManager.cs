using UnityEngine;

public class RoomTutorialManager : RoomManager
{
    private void Start()
    {
        Companion.Instance.ActivateCompanion();
    }

    public void SignalPlayIntroVoiceLine()
    {
        Debug.Log("play intro voice line");
        Companion.Instance.TriggerVoiceLine("tutorial");
    }
}
