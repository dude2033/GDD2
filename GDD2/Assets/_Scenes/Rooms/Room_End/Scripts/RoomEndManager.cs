using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class RoomEndManager : RoomManager
{
    public float transitionTime = 3;
    [SerializeField] private Volume TransitionFXVolume;

    public void StartTransitionToBlack()
    {
        StartCoroutine(FXTransitionCoroutine());
    }

    IEnumerator FXTransitionCoroutine()
    {
        float currentTime = 0;

        while (currentTime < transitionTime)
        {
            currentTime += Time.deltaTime;
            TransitionFXVolume.weight = Mathf.Lerp(0, 1, currentTime / transitionTime);
            yield return null;
        }
        yield return null;
    }
}
