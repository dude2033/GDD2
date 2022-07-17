using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class LocomotionConfiguration : MonoBehaviour
{
    public MovementType movementType;
    public roationType rotationType;

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

    public enum roationType
    {
        continuous,
        snap
    }

    private void OnValidate()
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
            case roationType.continuous:
                continuousTurnProvider.enabled = true;
                break;
            case roationType.snap:
                snapTurnProvider.enabled = true;
                break;
        }
    }
}
