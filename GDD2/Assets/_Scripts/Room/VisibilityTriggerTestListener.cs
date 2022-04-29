using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisibilityTriggerTestListener : MonoBehaviour
{
    public void LogPlusXEntered() => Debug.Log("PlusXEntered");
    public void LogPlusXExited() => Debug.Log("PlusXExited");
    public void LogMinusXEntered() => Debug.Log("MinusXEntered");
    public void LogMinusXExited() => Debug.Log("MinusXExited");
    
    public void LogPlusYEntered() => Debug.Log("PlusYEntered");
    public void LogPlusYExited() => Debug.Log("PlusYExited");
    public void LogMinusYEntered() => Debug.Log("MinusYEntered");
    public void LogMinusYExited() => Debug.Log("MinusYExited");
    
    public void LogPlusZEntered() => Debug.Log("PlusZEntered");
    public void LogPlusZExited() => Debug.Log("PlusZExited");
    public void LogMinusZEntered() => Debug.Log("MinusZEntered");
    public void LogMinusZExited() => Debug.Log("MinusZExited");
}
