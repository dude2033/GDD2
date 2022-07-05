using UnityEngine;

public class CableStartAutoDisable : MonoBehaviour
{
    [SerializeField] private GameObject cableStart;

    private void OnDisable() => cableStart.SetActive(false);
    private void OnEnable() => cableStart.SetActive(true);
    
}
