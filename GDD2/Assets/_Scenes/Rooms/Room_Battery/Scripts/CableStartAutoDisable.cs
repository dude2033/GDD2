using UnityEngine;

public class CableStartAutoDisable : MonoBehaviour
{
    [SerializeField] private GameObject cableStart;

    private void OnDisable()
    {
        if(cableStart != null)
            cableStart.SetActive(false);
    }

    private void OnEnable()
    {
        if(cableStart != null)
            cableStart.SetActive(true);
    }
  
}
