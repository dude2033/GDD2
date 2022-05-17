using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;
using UnityEngine.VFX;

public class RadioAction : MonoBehaviour
{
    [SerializeField] private PlayableDirector playableDirector;
    [SerializeField] private MeshRenderer radioMeshRenderer;
    [SerializeField] private VisualEffect flyingNotesVFX;
    [SerializeField] private float transitionDuration = 1.0f;

    private Material radioMat;
    private static readonly int DisplacementAmount = Shader.PropertyToID("displacementAmount");

    private void Awake()
    {
        radioMat = radioMeshRenderer.material;
    }
    
    [ContextMenu("start timeline")]
    public void StartTimeline()
    {
        playableDirector.Play();
    }

    public void ReceivedSignalStopRadioAnimation()
    {
        StartCoroutine(nameof(StopRadioAnimation));
    }

    IEnumerator StopRadioAnimation()
    {
        float currentTime = 0;
        Vector3 startDisplacementAmount = radioMat.GetVector(DisplacementAmount);
        Vector3 newDisplacementAmount = new Vector3();

        while (currentTime < transitionDuration)
        {
            newDisplacementAmount.x = Mathf.Lerp(startDisplacementAmount.x, 0, currentTime / transitionDuration);
            newDisplacementAmount.y = Mathf.Lerp(startDisplacementAmount.y, 0, currentTime / transitionDuration);
            newDisplacementAmount.z = Mathf.Lerp(startDisplacementAmount.z, 0, currentTime / transitionDuration);
            
            radioMat.SetVector(DisplacementAmount, newDisplacementAmount);
            currentTime += Time.deltaTime;
            
            yield return null;
        }

        radioMat.SetVector(DisplacementAmount, Vector4.zero);
        flyingNotesVFX.enabled = false;
        yield return null;
    }
    
}
