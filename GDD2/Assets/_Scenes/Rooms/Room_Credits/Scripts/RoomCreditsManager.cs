public class RoomCreditsManager : RoomManager
{
    void Start()
    {
        Companion.Instance.DisableCompanion();
    }

    private void OnEnable()
    {
        Companion.Instance.DisableCompanion();
    }
}
