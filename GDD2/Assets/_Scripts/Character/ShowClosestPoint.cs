using TMPro;
using UnityEngine;

// Note that closestPoint is based on the surface of the collider
// and location represents a point in 3d space.
// The gizmos work in the editor.
//
// Create an origin-based cube and give it a scale of (1, 0.5, 3).
// Change the BoxCollider size to (0.8, 1.2, 0.8).  This means that
// collisions will happen when a GameObject gets close to the BoxCollider.
// The ShowClosestPoint.cs script shows spheres that display the location
// and closestPoint.  Try changing the BoxCollider size and the location
// values.

// Attach this to a GameObject that has a Collider component attached
public class ShowClosestPoint : MonoBehaviour
{
    public Vector3 location;
    public Transform target;

    public void OnDrawGizmos()
    {
        var collider = GetComponent<Collider>();

        if (!collider)
        {
            return; // nothing to do without a collider
        }

        Vector3 closestPoint = collider.ClosestPoint(target.position);
        
        Debug.Log("transform: " + target.position + " closest: " + closestPoint);



        Gizmos.color = target.position.Compare(closestPoint, 100) ? Color.red : Color.green;
        Gizmos.DrawSphere(target.position, 0.1f);
        Gizmos.DrawWireSphere(closestPoint, 0.1f);
    }
}