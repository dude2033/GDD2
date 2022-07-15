using System;
using System.Collections;
using System.Collections.Generic;
using FMODUnity;
using Unity.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.VFX;

public class ButtonPanel : MonoBehaviour
{
    [SerializeField] private int code = 1024;
    [SerializeField, ReadOnly] private int currentCode = 0;
    [SerializeField] private StudioEventEmitter validBeepEmitter;
    [SerializeField] private StudioEventEmitter invalidBeepEmitter;
    [SerializeField] private VisualEffect flyingNumbersVFX;
    public UnityEvent codeComplete;
    private int digitIndex = 1000;
    private int digitIndexMax = 1000;

    private void Start()
    {
        digitIndexMax = 1;
        int tmpCode = code;
        while (tmpCode >= 10)
        {
            digitIndexMax *= 10;
            tmpCode /= 10;
        }
        digitIndex = digitIndexMax;
        ShowNextNumber();
    }

    public void AddNumber(int number)
    {
        currentCode += digitIndex * number;

        if (CheckCode())
            HandleCodeValid();
        else
            HandleCodeInvalid();
    }

    private void HandleCodeValid()
    {
        digitIndex /= 10;
        if (digitIndex <= 0)
            CodeComplete();

        ShowNextNumber();
    }

    private void HandleCodeInvalid()
    {
        invalidBeepEmitter.Play();
        currentCode = 0;
        digitIndex = digitIndexMax;
    }

    private void ShowNextNumber()
    {
        // calculate next digit
        // set VFX property
    }
    
    private bool CheckCode()
    {
        return code - code % digitIndex == currentCode;
    }

    private void CodeComplete()
    {
        validBeepEmitter.Play();
        codeComplete.Invoke();
    }
    
    [ContextMenu("0")]
    public void AddZero() => AddNumber(0);

    [ContextMenu("1")]
    public void AddOne() => AddNumber(1);
    
    [ContextMenu("2")]
    public void AddTwo() => AddNumber(2);

    [ContextMenu("3")]
    public void AddThree() => AddNumber(3);
    
    [ContextMenu("4")]
    public void AddFour() => AddNumber(4);

    [ContextMenu("5")]
    public void AddFive() => AddNumber(5);
    
    [ContextMenu("6")]
    public void AddSix() => AddNumber(6);

    [ContextMenu("7")]
    public void AddSeven() => AddNumber(7);
    
    [ContextMenu("8")]
    public void AddEight() => AddNumber(8);

    [ContextMenu("9")]
    public void AddNine() => AddNumber(9);
}
