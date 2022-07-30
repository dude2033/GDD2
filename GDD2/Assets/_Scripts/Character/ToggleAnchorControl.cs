using System;
using System.Collections;
using System.Collections.Generic;
using ParadoxNotion.Serialization.FullSerializer;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

public class ToggleAnchorControl : MonoBehaviour
{
    [SerializeField] private InputActionAsset inputActions;
    [SerializeField] private string actionMap = "XRI LeftHand Interaction";

    [Header("State ray color indication")] 
    [SerializeField] private Gradient validColorGradientAnchorControl;
    [SerializeField] private Gradient invalidColorGradientAnchorControl;
    [SerializeField] private Gradient validColorGradientNoAnchorControl;
    [SerializeField] private Gradient invalidColorGradientNoAnchorControl;
    
    private InputAction primaryButton;
    
    public UnityEvent<bool> anchorControlStateChanged;
    
    private XRRayInteractor rayInteractor;
    private XRInteractorLineVisual lineVisual;
    private bool controlState = false;

    private void Start()
    {
        primaryButton = inputActions.FindActionMap(actionMap).FindAction("Joystick Click");
        primaryButton.performed += PrimaryButtonPressed;
        primaryButton.Enable();
        rayInteractor = GetComponent<XRRayInteractor>();
        lineVisual = GetComponent<XRInteractorLineVisual>();
        SetAnchorControlState();
    }

    private void PrimaryButtonPressed(InputAction.CallbackContext obj)
    {
        controlState = !controlState;
        SetAnchorControlState();
    }


    private void SetAnchorControlState()
    {
        rayInteractor.allowAnchorControl = controlState;
        anchorControlStateChanged.Invoke(!controlState);
        SetLineVisual();
    }

    private void SetLineVisual()
    {
        if (controlState)
        {
            lineVisual.validColorGradient = validColorGradientAnchorControl;
            lineVisual.invalidColorGradient = invalidColorGradientAnchorControl;
        }
        else
        {
            lineVisual.validColorGradient = validColorGradientNoAnchorControl;
            lineVisual.invalidColorGradient = invalidColorGradientNoAnchorControl;
        }
    }
    
}
