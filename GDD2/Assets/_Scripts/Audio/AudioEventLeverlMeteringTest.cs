using System.Collections;
using System.Collections.Generic;
using FMOD;
using UnityEngine;
using Debug = UnityEngine.Debug;

[RequireComponent(typeof(AudioEventLevelListener))]
public class AudioEventLeverlMeteringTest : MonoBehaviour
{
    private AudioEventLevelListener listener;
    private DSP_METERING_INFO outputInfo;
    
    void Start()
    {
        listener = GetComponent<AudioEventLevelListener>();
    }

    void Update()
    {
        listener.GetMeteringInfo(out outputInfo);
        Debug.Log(outputInfo.rmslevel[0]);
    }
}
