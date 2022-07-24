using UnityEngine;

public class CableEndData : MonoBehaviour
{
    public Transform parentTransform;
    public int parentInstanceID;
    // Start is called before the first frame update
    void Awake()
    {
        parentTransform = transform.parent;
        parentInstanceID = transform.parent.GetInstanceID();
    }
}
