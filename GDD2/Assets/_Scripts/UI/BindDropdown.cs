using System;
using TMPro;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class BindDropdown : MonoBehaviour
{
    [SerializeField] private TMP_Dropdown movementDropdown;
    [SerializeField] private TMP_Dropdown rotationDropdown;
    
    // Start is called before the first frame update
    void Start()
    {
        movementDropdown.onValueChanged.AddListener(ChangeMovementType);
        rotationDropdown.onValueChanged.AddListener(ChangeRotationType);

        movementDropdown.value = (int)LocomotionConfiguration.Instance.movementType;
        rotationDropdown.value = (int)LocomotionConfiguration.Instance.rotationType;
    }

    private void ChangeRotationType(int value)
    {
        LocomotionConfiguration.Instance.rotationType = (LocomotionConfiguration.RoationType) Enum.ToObject(typeof(LocomotionConfiguration.RoationType), value);
        LocomotionConfiguration.Instance.ApplySettings();
    }

    private void ChangeMovementType(int value)
    {
        LocomotionConfiguration.Instance.movementType = (LocomotionConfiguration.MovementType) Enum.ToObject(typeof(LocomotionConfiguration.MovementType), value);
        LocomotionConfiguration.Instance.ApplySettings();
    }
}
