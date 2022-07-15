// taken from https://www.youtube.com/watch?v=HFNzVMi5MSQ

using System;
using System.Collections;
using System.Collections.Generic;
using NodeCanvas.Tasks.Conditions;
using UnityEngine;
using UnityEngine.Events;

public class PhysicsButton : MonoBehaviour
{
    [SerializeField] private float threshold = 0.1f;
    [SerializeField] private float deadZone = 0.025f;
    [SerializeField] private float maxLinear = 0.02f;
    [SerializeField] private float minLinear = -0.006f;
    [SerializeField] private direction pushDirection = direction.Y;
    enum direction
    {
        X,Y,Z,None
    }
    
    public UnityEvent onPressed, onReleased;

    private bool isPressed;
    private Vector3 startPosition;
    private ConfigurableJoint joint;
    
    // Start is called before the first frame update
    void Start()
    {
        startPosition = transform.localPosition;
        joint = GetComponent<ConfigurableJoint>();
    }

    // Update is called once per frame
    void Update()
    {
        if(!isPressed && GetValue() + threshold >= 1)
            Pressed();
        if(isPressed && GetValue() - threshold <= 0)
            Released();
        
        Vector3 currentPos = transform.localPosition;
        switch (pushDirection)
        {
            case direction.X:
                currentPos.x = Mathf.Clamp(currentPos.x, minLinear, maxLinear);
                break;
            case direction.Y:
                currentPos.y = Mathf.Clamp(currentPos.y, minLinear, maxLinear);
                break;
            case direction.Z:
                currentPos.z = Mathf.Clamp(currentPos.z, minLinear, maxLinear);
                break;
        }
        transform.localPosition = currentPos;
    }

    private float GetValue()
    {
        float value = Vector3.Distance(startPosition, transform.localPosition) / joint.linearLimit.limit;

        if (Math.Abs(value) < deadZone)
            value = 0;

        return Mathf.Clamp(value, -1f, 1f);
    }

    private void Pressed()
    {
        isPressed = true;
        onPressed.Invoke();
    }
    
    private void Released()
    {
        isPressed = false;
        onReleased.Invoke();
    }
}
