using System.Collections;
using System.Collections.Generic;
using NodeCanvas.Framework;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class GUIControls : MonoBehaviour
{
    [Header("Visibility Events")]
    [SerializeField] private SignalDefinition onPlusXEnter;
    [SerializeField] private SignalDefinition onPlusXExit;
    [SerializeField] private SignalDefinition onPlusYEnter;
    [SerializeField] private SignalDefinition onPlusYExit;
    [SerializeField] private SignalDefinition onPlusZEnter;
    [SerializeField] private SignalDefinition onPlusZExit;
    
    [SerializeField] private SignalDefinition onMinusXEnter;
    [SerializeField] private SignalDefinition onMinusXExit;
    [SerializeField] private SignalDefinition onMinusYEnter;
    [SerializeField] private SignalDefinition onMinusYExit;
    [SerializeField] private SignalDefinition onMinusZEnter;
    [SerializeField] private SignalDefinition onMinusZExit;
    
    [Header("Companion")]
    [SerializeField] private SignalDefinition advanceHintIndex;
    
    [Header("Room FSM")]
    [SerializeField] private SignalDefinition advanceRoomFSM;

    [Header("XR Origin")] 
    [SerializeField] private Transform XROrigin;

    void OnGUI()
    {
        // visibility trigger
        int gap = 25;
        int currentY = 5;
        GUI.Label(new Rect(10, currentY, 140, 20), "Visibility trigger");
        
        if (GUI.Button(new Rect(10, currentY+=gap, 70, 20), "+X Enter"))
            onPlusXEnter.Invoke(null, null, true);
        if (GUI.Button(new Rect(80, currentY, 70, 20), "+X Exit"))
            onPlusXExit.Invoke(null, null, true);
        
        if (GUI.Button(new Rect(10, currentY+=gap, 70, 20), "-X Enter"))
            onMinusXEnter.Invoke(null, null, true);
        if (GUI.Button(new Rect(80, currentY, 70, 20), "-X Exit"))
            onMinusXExit.Invoke(null, null, true);

        if (GUI.Button(new Rect(10, currentY+=gap, 70, 20), "+Y Enter"))
            onPlusYEnter.Invoke(null, null, true);
        if (GUI.Button(new Rect(80, currentY, 70, 20), "+Y Exit"))
            onPlusYExit.Invoke(null, null, true);
        
        if (GUI.Button(new Rect(10, currentY+=gap, 70, 20), "-Y Enter"))
            onMinusYEnter.Invoke(null, null, true);
        if (GUI.Button(new Rect(80, currentY, 70, 20), "-Y Exit"))
            onMinusYExit.Invoke(null, null, true);
        
        if (GUI.Button(new Rect(10, currentY+=gap, 70, 20), "+Z Enter"))
            onPlusZEnter.Invoke(null, null, true);
        if (GUI.Button(new Rect(80, currentY, 70, 20), "+Z Exit"))
            onPlusZExit.Invoke(null, null, true);
        
        if (GUI.Button(new Rect(10, currentY+=gap, 70, 20), "-Z Enter"))
            onMinusZEnter.Invoke(null, null, true);
        if (GUI.Button(new Rect(80, currentY, 70, 20), "-Z Exit"))
            onMinusZExit.Invoke(null, null, true);
        
        // companion
        GUI.Label(new Rect(10, currentY+=gap, 140, 20), "Companion");
        if (GUI.Button(new Rect(10, currentY+=gap, 140, 20), "get Hint"))
            advanceHintIndex.Invoke(null, null, true);
        
        // scene
        GUI.Label(new Rect(10, currentY+=gap, 140, 20), "Scene");
        if (GUI.Button(new Rect(10, currentY+=gap, 140, 20), "advance Room FSM"))
            advanceRoomFSM.Invoke(null, null, true);
        if (GUI.Button(new Rect(10, currentY+=gap, 140, 20), "next scene"))
            SceneProgressionManager.Instance.ProgressWithUnloading();
        if (GUI.Button(new Rect(10, currentY += gap, 140, 20), "origin up"))
            XROrigin.localPosition = new Vector3(0, 1.4f, 0);

    }
}
