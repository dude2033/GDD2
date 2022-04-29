using System;
using System.Collections;
using FMOD;
using FMOD.Studio;
using FMODUnity;
using UnityEngine;
using Debug = UnityEngine.Debug;

public class AudioEventLevelListener : MonoBehaviour
{
    [SerializeField] private StudioEventEmitter studioEventEmitter;
    // public FMOD.DSP_METERING_INFO outputInfo;

    private EventInstance eventInstance;
    private FMOD.ChannelGroup channelGroup;
    private FMOD.DSP channelDSP;
    private bool doUpdate = false;
    
    void Start()
    {
        // coroutine needed because the initialization needs some time
        StartCoroutine(nameof(SetupCoroutine));
    }

    IEnumerator SetupCoroutine()
    {
        eventInstance = studioEventEmitter.EventInstance;
        if(eventInstance.isValid())
        {
            while (eventInstance.getChannelGroup(out channelGroup) != FMOD.RESULT.OK)
            {
                yield return new WaitForEndOfFrame();
            }
            
            channelGroup.getDSP (0, out channelDSP);
            channelDSP.setMeteringEnabled (false, true);
            // outputInfo = new FMOD.DSP_METERING_INFO();
            doUpdate = true;
        }
        else
        {
            Debug.LogError("Event instance no valid");
            yield return null;
        }
    }
    
    // void Update()
    // {
    //     // access the private outputInfo variable to get the rms and peak values
    //     // e.g. float rmsLeft = outputInfo.rmslevel[0];
    //     if (channelDSP.getMeteringInfo(new IntPtr(), out outputInfo) != FMOD.RESULT.OK)
    //     {
    //         if(studioEventEmitter.IsActive)
    //             StartCoroutine(nameof(SetupCoroutine));
    //     }
    // }

    public FMOD.RESULT GetMeteringInfo(out DSP_METERING_INFO outputInfo)
    {
        FMOD.RESULT result = channelDSP.getMeteringInfo(new IntPtr(), out outputInfo);
        
        if (result != FMOD.RESULT.OK)
        {
            if(studioEventEmitter.IsActive)
                StartCoroutine(nameof(SetupCoroutine));
        }

        return result;
    }
}


