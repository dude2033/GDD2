using System;
using FMODUnity;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.VFX;

public class ButtonPanel : MonoBehaviour
{
    [SerializeField] private string code = "1024";
    [SerializeField] private StudioEventEmitter validBeepEmitter;
    [SerializeField] private StudioEventEmitter invalidBeepEmitter;
    [SerializeField] private VisualEffect flyingNumbersVFX;
    
    public UnityEvent codeComplete;

    private bool solved = false;
    private string currentCode = "";
    private int numDigits = 4;
    private int digitIndex = 0;
    private bool isActive = false;

    private void Start()
    {
        numDigits = code.Length;
        ShowNextDigit();
        Invoke(nameof(SetActive), 1f);
    }

    private void SetActive() => isActive = true;

    public void AddNumber(int number)
    {
        if(solved || !isActive)
            return;

        currentCode += number.ToString();

        if (CheckCode())
            HandleCodeValid();
        else
            HandleCodeInvalid();
        
        ShowNextDigit();
    }

    private void HandleCodeValid()
    {
        digitIndex++;
        if(digitIndex >= numDigits)
            CodeComplete();
    }

    private void HandleCodeInvalid()
    {
        currentCode = "";
        digitIndex = 0;
        invalidBeepEmitter.Play();
    }

    private void ShowNextDigit()
    {
        if(digitIndex >= code.Length)
            return;
        
        // ascii to int conversion
        int nextDigit = code[digitIndex] - '0';
        flyingNumbersVFX.SetInt("NumberIndex", nextDigit);
    }
    
    private bool CheckCode()
    {
        return String.Compare(code, 0, currentCode, 0, digitIndex + 1) == 0;
    }

    private void CodeComplete()
    {
        solved = true;
        validBeepEmitter.Play();
        codeComplete.Invoke();
    }

    #region Debug functions
    
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
    
    #endregion
}
