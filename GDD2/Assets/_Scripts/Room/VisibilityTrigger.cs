using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;
using UnityEngine.Events;

/*
 * This script fires an event whenever the player starts or ends looking at a certain section of the room. 
 */
public class VisibilityTrigger : MonoBehaviour
{
    [SerializeField] private Transform playerCamera;

    [Header("Events")]
    public UnityEvent onPlusXEnter;
    public UnityEvent onPlusXExit;
    
    public UnityEvent onMinusXEnter;
    public UnityEvent onMinusXExit;
    
    public UnityEvent onPlusYEnter;
    public UnityEvent onPlusYExit;
    
    public UnityEvent onMinusYEnter;
    public UnityEvent onMinusYExit;
    
    public UnityEvent onPlusZEnter;
    public UnityEvent onPlusZExit;
    
    public UnityEvent onMinusZEnter;
    public UnityEvent onMinusZExit;

    private Vector3 lastSnappedCamForward = Vector3.zero;
    private Vector3 snappedCamForward = Vector3.zero;
    
    // Start is called before the first frame update
    void Start()
    {
        if(!playerCamera)
            playerCamera = Camera.main.transform;
    }

    // Update is called once per frame
    void Update()
    {
        CheckVisibilites();
    }

    /*
     * checks if the visible wall has changed and triggers the corresponding event
     */
    private void CheckVisibilites()
    {
        snappedCamForward = SnapDirection(playerCamera.forward);

        if (lastSnappedCamForward != snappedCamForward)
        {
            TriggerEvent(snappedCamForward);
            TriggerEvent(lastSnappedCamForward, false);
            lastSnappedCamForward = snappedCamForward;
        }
    }

    /*
     * snaps the direction to one of the axis (x, y or z)
     */
    private Vector3 SnapDirection(Vector3 vec)
    {
        int largestIndex = 0;
        for (int i = 1; i < 3; i++)
        {
            largestIndex = Mathf.Abs(vec[i]) > Mathf.Abs(vec[largestIndex]) ? i : largestIndex;
        }
        
        Vector3 snappedDirection = Vector3.zero;
        snappedDirection[largestIndex] = 1 * Mathf.Sign(vec[largestIndex]);
        return snappedDirection;
    }

    /*
     * return the corresponding event based on the snappedDirection variable
     * entered = true : trigger entered event
     * entered = false : trigger exited event
     */
    private void TriggerEvent(Vector3 snappedDirection, bool entered = true)
    {
        UnityEvent EventToTrigger = null;

        if (!Mathf.Approximately(snappedDirection[2], 0))
        {
            if (snappedDirection[2] > 0)
                EventToTrigger = entered ? onPlusZEnter : onPlusZExit;
            else
                EventToTrigger = entered ? onMinusZEnter : onMinusZExit;
        }

        if (!Mathf.Approximately(snappedDirection[1], 0))
        {
            if (snappedDirection[1] > 0)
                EventToTrigger = entered ? onPlusYEnter : onPlusYExit;
            else
                EventToTrigger = entered ? onMinusYEnter : onMinusYExit;
        }
        
        if (EventToTrigger == null)
        {
            if (snappedDirection[0] > 0)
                EventToTrigger = entered ? onPlusXEnter : onPlusXExit;
            else
                EventToTrigger = entered ? onMinusXEnter : onMinusXExit;
        }
        
        EventToTrigger.Invoke();
    }
}
