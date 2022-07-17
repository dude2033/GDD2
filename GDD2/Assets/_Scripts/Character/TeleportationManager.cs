using System;
using System.Collections;
using System.Collections.Generic;
using ParadoxNotion.Serialization.FullSerializer;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Inputs;

// taken from: https://www.youtube.com/watch?v=cxRnK8aIUSI&t=687s
public class TeleportationManager : MonoBehaviour
{
    public InputActionAsset actionAsset;
    public XRRayInteractor rayInteractor;
    public TeleportationProvider provider;
    private InputAction thumbstick;
    private bool isActive;
    
    // Start is called before the first frame update
    void Start()
    {
        var activate = actionAsset.FindActionMap("XRI LeftHand Locomotion").FindAction("Teleport Mode Activate");
        activate.Enable();
        activate.performed += OnTeleportActivate;
        
        var cancel = actionAsset.FindActionMap("XRI LeftHand Locomotion").FindAction("Teleport Mode Cancel");
        cancel.Enable();
        cancel.performed += OnTeleportCancel;

        thumbstick = actionAsset.FindActionMap("XRI LeftHand Locomotion").FindAction("Move");
        thumbstick.Enable();
    }

    private void Update()
    {
        if(!isActive)
            return;

        if (thumbstick.triggered)
            return;

        if (!rayInteractor.TryGetCurrent3DRaycastHit(out RaycastHit hit))
        {
            rayInteractor.lineType = XRRayInteractor.LineType.StraightLine;
            isActive = false;
            return;
        }

        TeleportRequest request = new TeleportRequest()
        {
            destinationPosition = hit.point
        };
        provider.QueueTeleportRequest(request);
        rayInteractor.lineType = XRRayInteractor.LineType.StraightLine;
        isActive = false;
    }

    private void OnTeleportActivate(InputAction.CallbackContext context)
    {
        rayInteractor.lineType = XRRayInteractor.LineType.ProjectileCurve;
        isActive = true;
    }

    private void OnTeleportCancel(InputAction.CallbackContext context)
    {
        rayInteractor.lineType = XRRayInteractor.LineType.StraightLine;
        isActive = false;
    }
    
}
