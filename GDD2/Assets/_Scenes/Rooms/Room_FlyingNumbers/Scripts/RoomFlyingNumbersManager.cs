using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomFlyingNumbersManager : RoomManager
{
    [SerializeField] private GameObject flyingNumbers;
    public void SetFlyingNumbers(bool state)
    {
        flyingNumbers.SetActive(state);
    }
}
