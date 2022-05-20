using UnityEngine;
using GD.MinMaxSlider;

public class CompanionPositionController : MonoBehaviour
{
    [Header("Parameter personalization")] 
    [SerializeField] private Transform targetTransform;
    [SerializeField] private float radius = 1.0f;
    [SerializeField] private float height = 1.3f;
    [SerializeField] private float forceMultiplier = 50;
    [SerializeField] private ForceMode mode = ForceMode.Impulse;
    
    [MinMaxSlider(0,2)] 
    public Vector2 minMaxDistance = new Vector2(0.5f, 2f);
    
    [Header("Companion")]
    [SerializeField] private Transform companionTransform;
    
    private Vector3 offsetPoint = Vector3.zero;
    private Rigidbody rb;
    private bool hitRadiusBound = false;
    
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        if (targetTransform == null)
        {
            targetTransform = Camera.main.transform;
        }
        offsetPoint = new Vector3(0, height, 0);
    }
    
    private void FixedUpdate()
    {
        Vector3 directionToTarget = companionTransform.position - targetTransform.position;
        float distanceToTarget = Vector3.Magnitude(directionToTarget);
        directionToTarget.Normalize();
        
        
        float targetForceToApply = distanceToTarget;

        if (distanceToTarget < minMaxDistance.x)
        {
            float attenuation = Mathf.Clamp((minMaxDistance.x - distanceToTarget) * 10,1, 5);
            rb.AddForce(directionToTarget * (targetForceToApply * attenuation * forceMultiplier), mode);
        }
        else
            rb.AddForce(-directionToTarget * (targetForceToApply * forceMultiplier), mode);
        
        Vector3 directionToCenter = companionTransform.position - offsetPoint;
        float distanceToCenter = Vector3.Magnitude(directionToCenter);
        
        if (distanceToCenter > radius)
        {
            directionToCenter.Normalize();
            float forceToApply = 2 * distanceToCenter - radius;
            rb.AddForce(-directionToCenter * (forceMultiplier * (forceToApply * (1 + 10 * (distanceToCenter - radius)))), mode);

            if (!hitRadiusBound)
            {
                Vector3 normalDirection = Vector3.Cross(Vector3.up, -directionToCenter).normalized;
                rb.AddForce(normalDirection, ForceMode.Impulse);
                hitRadiusBound = true;
            }
        }
        else
        {
            hitRadiusBound = false;
        }
    }
    
    private void OnValidate()
    {
        transform.position = new Vector3(transform.position.x, height, transform.position.z);
        offsetPoint = new Vector3(0, height, 0);
    }

    // void OnDrawGizmosSelected()
    // {
    //     UnityEditor.Handles.color = Color.green;
    //     UnityEditor.Handles.DrawWireDisc(offsetPoint , transform.up, radius);
    //     UnityEditor.Handles.color = Color.red;
    //     Gizmos.DrawSphere(targetPoint, 0.1f);
    //     Gizmos.DrawSphere(targetTransform.position, 0.1f);
    //     UnityEditor.Handles.color = Color.blue;
    //     Gizmos.DrawWireSphere(companionTransform.position, minMaxDistance.x);
    //     UnityEditor.Handles.color = Color.magenta;
    //     Gizmos.DrawWireSphere(companionTransform.position, minMaxDistance.y);
    // }
}
