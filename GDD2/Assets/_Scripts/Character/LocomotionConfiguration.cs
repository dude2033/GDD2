using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class LocomotionConfiguration : Singleton<LocomotionConfiguration>
{
    public MovementType movementType;
    public RoationType rotationType;

    [Header("Movement")]
    [SerializeField] private TeleportationProvider teleportationProvider;
    [SerializeField] private TeleportationManager teleportationManager;
    [SerializeField] private ContinuousMoveProviderBase continuousMoveProvider;

    [Header("Rotation")] 
    [SerializeField] private ContinuousTurnProviderBase continuousTurnProvider;
    [SerializeField] private SnapTurnProviderBase snapTurnProvider;
    
    
    public enum MovementType
    {
        continuous,
        teleportation
    }

    public enum RoationType
    {
        continuous,
        snap
    }

    private void OnValidate()
    {
        ApplySettings();
    }

    public void ApplySettings()
    {
        teleportationProvider.enabled = false;
        teleportationManager.enabled = false;
        continuousMoveProvider.enabled = false;
        continuousTurnProvider.enabled = false;
        snapTurnProvider.enabled = false;
        
        switch (movementType)
        {
            case MovementType.continuous:
                continuousMoveProvider.enabled = true;
                break;
            case MovementType.teleportation:
                teleportationProvider.enabled = true;
                teleportationManager.enabled = true;
                break;
        }

        switch (rotationType)
        {
            case RoationType.continuous:
                continuousTurnProvider.enabled = true;
                break;
            case RoationType.snap:
                snapTurnProvider.enabled = true;
                break;
        }
    }
    

    protected override void OnApplicationQuitCallback()
    {
    }

    protected override void OnEnableCallback()
    {
    }
}
