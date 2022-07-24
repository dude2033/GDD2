using System;
using System.Collections;
using System.Collections.Generic;
using FMODUnity;
using UnityEngine;

public class SkipFirstCollisionAudio : MonoBehaviour
{
    [SerializeField] private StudioEventEmitter emitter;

    private void Awake()
    {
        emitter.enabled = false;
    }

    private void OnCollisionEnter(Collision other)
    {
        Invoke(nameof(EnableEmitter), 0.5f);
    }

    private void EnableEmitter()
    {
        emitter.enabled = true;
        emitter.PlayEvent = EmitterGameEvent.CollisionEnter;
    }
}
