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
public class RoomBatterieManager : RoomManager
{
    [SerializeField] private Material standardRoomMaterial;
    public bool lightsOn = false;
    public bool suppressMaterialInvert = false;

    [SerializeField] private GameObject roomGO;
    [SerializeField] private GameObject puzzleGO;

    private Material roomMat;
    private Material puzzleMat;
    private bool roomDone = false;

    private void Start()
    {
        roomMat = roomGO.GetComponent<MeshRenderer>().materials[0];
        puzzleMat = puzzleGO.GetComponent<MeshRenderer>().materials[0];
    }
    
    public void SetLights(bool state)
    {
        if(suppressMaterialInvert)
            return;
        
        int numberState = state ? 0 : 1;
        lightsOn = state;
        roomMat.SetInt("Invert", numberState);
        puzzleMat.SetInt("Invert", numberState);
    }
    
    public void SetRoomMaterialWhite()
    {
        suppressMaterialInvert = true;
        roomMat.SetInt("Invert", 0);
    }
}