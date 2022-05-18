using UnityEngine;

public class RoomManager : MonoBehaviour
{
    [ContextMenu("Proceed to next room")]
    public void ProceedToNextRoom()
    {
        SceneProgressionManager.Instance.Progress();
    }
}
