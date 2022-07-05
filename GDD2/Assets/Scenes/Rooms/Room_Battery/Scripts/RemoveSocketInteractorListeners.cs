using UnityEngine;
using UnityEngine.Events;
using UnityEngine.XR.Interaction.Toolkit;

[RequireComponent(typeof(XRSocketInteractor))]
public class RemoveSocketInteractorListeners : MonoBehaviour
{
    [ContextMenu("Remove Listeners")]
    public void RemoveListeners()
    {
        Debug.Log("remove listeners");
        XRSocketInteractor socketInteractor = GetComponent<XRSocketInteractor>();
        // socketInteractor.interactionManager.IXRSelectInteractor;
        IXRSelectInteractor ix = GetComponent<IXRSelectInteractor>();
        Debug.Log(ix);
        Debug.Log(ix);
        // IXRSelectInteractor
        // socketInteractor.onSelectEntered.RemoveAllListeners();
        // socketInteractor.onSelectExited.RemoveAllListeners();
        // socketInteractor.onSelectEnter.RemoveAllListeners();
        // socketInteractor.onSelectExit.RemoveAllListeners();
    }
}
