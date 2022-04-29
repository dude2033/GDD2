using UnityEngine;
using UnityEngine.Events;
using UnityEngine.XR.Interaction.Toolkit;

public class CableSocketPair : MonoBehaviour
{
    [SerializeField] private XRSocketInteractor cableSocketA, cableSocketB;
    public bool isConnected = false;

    public UnityEvent connectionEstablishedEvent;

    public void checkConnection()
    {
        isConnected = cableSocketA.GetOldestInteractableSelected()?.transform.GetComponent<CableEndData>().parentInstanceID == 
                      cableSocketB.GetOldestInteractableSelected()?.transform.GetComponent<CableEndData>().parentInstanceID;
        
        if(isConnected)
            connectionEstablishedEvent.Invoke();
    }

}
