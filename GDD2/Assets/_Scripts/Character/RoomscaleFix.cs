// taken from https://www.youtube.com/watch?v=pgX2tLIXNZ8

using Unity.XR.CoreUtils;
using UnityEngine;

public class RoomscaleFix : MonoBehaviour
{
    private CharacterController character;
    private XROrigin xrOrigin;
    
    
    void Start()
    {
        character = GetComponent<CharacterController>();
        xrOrigin = GetComponent<XROrigin>();
    }

    private void FixedUpdate()
    {
        character.height = xrOrigin.CameraInOriginSpaceHeight + 0.15f;
        
        var centerPoint = transform.InverseTransformPoint(xrOrigin.Camera.transform.position);
        character.center = new Vector3(centerPoint.x, character.height / 2 + character.skinWidth, centerPoint.z);

        // wiggle the controller a bit so that the collisions get recalculated every fixed step
        // unity checks if the player is allowed to move there
        character.Move(new Vector3(+0.001f, -0.001f, +0.001f));
        character.Move(new Vector3(-0.001f, 0.001f, -0.001f));
    }
}
