using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

/*
 * Player needs to place the battery inside the corresponding slot to turn the lights on
 * Battery spawns after full rotation using the visibility triggers
 * same hold for the battery socket
 */

/*
 * Progression:
 * look first at -z wall
 * look second time at -z wall -> spawn battery at +z
 * look third time at -z -> spawn the socket
 * place the battery inside the socket
 * invert the shader colors to indicate light
 */
public class RoomBatterieManager : MonoBehaviour
{
    private enum RoomState
    {
        IDLE,
        START,
        FIRSTLOOK,
        SECONDLOOK,
        THIRDLOOK,
        DONE
    }

    [SerializeField] private GameObject roomGO;
    [SerializeField] private GameObject batteryGO;
    [SerializeField] private GameObject batterySocketGO;

    private RoomState roomState = RoomState.IDLE;
    private int maxNumberOfStates;
    private Material roomMat;
    private bool roomDone = false;

    private void Start()
    {
        roomMat = roomGO.GetComponent<MeshRenderer>().materials[0];
        maxNumberOfStates = Enum.GetValues(typeof(RoomState)).Length;
        batteryGO.SetActive(false);
        batterySocketGO.SetActive(false);

        // for debugging - normally this should be called from the previous room
        StartRoom();
    }
    
    public void HandleStateVisualTrigger()
    {
        switch (roomState)
        {
            case RoomState.FIRSTLOOK:
                IncreaseState();
                break;
            case RoomState.SECONDLOOK:
                HandleSecondLook();
                IncreaseState();
                break;
            case RoomState.THIRDLOOK:
                HandleThirdLook();
                IncreaseState();
                break;
        }
    }

    [ContextMenu("Increase State")]
    public void IncreaseState()
    {
        roomState++;
        if ((int)roomState == maxNumberOfStates)
            roomState = RoomState.IDLE;
    }

    [ContextMenu("Start Room")]
    public void StartRoom()
    {
        roomState = RoomState.FIRSTLOOK;
    }

    private void HandleSecondLook()
    {
        // spawn battery
        batteryGO.SetActive(true);
    }
    
    private void HandleThirdLook()
    {
        // spawn socket
        batterySocketGO.SetActive(true);
    }
    
    public void HandleDone()
    { 
        // object placed inside socket
        // do something
        roomDone = true;
    }

    public void SetLights(bool state)
    {
        int numberState = state ? 0 : 1;
        roomMat.SetInt("Invert", numberState);
        
        if(!roomDone)
            HandleDone();
    }
}