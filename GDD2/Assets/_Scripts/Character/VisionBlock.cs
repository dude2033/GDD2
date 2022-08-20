using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class VisionBlock : MonoBehaviour
{
    [SerializeField] private BoxCollider boundingBox;
    [SerializeField] private Transform target;
    [SerializeField] private RawImage blockImage;
    [SerializeField] private float blockDistanceOffset = 0.2f;
    [SerializeField] private float blockDistanceFalloff = 0.1f;

    private void Start()
    {
        boundingBox.size -= Vector3.one * blockDistanceOffset;
    }


    // Update is called once per frame
    void Update()
    {
        Vector3 closestPoint = boundingBox.ClosestPoint(target.position);

        float distance = Vector3.Distance(closestPoint, target.position);
        // out of bounds
        if (distance.Equals(0))
        {
            blockImage.color = Color.clear;
        }
        else
        {
            Color targetColor = Color.black;
            targetColor.a = Mathf.Lerp(0, 1, distance / blockDistanceFalloff);
            blockImage.color = targetColor;
        }
    }
}
