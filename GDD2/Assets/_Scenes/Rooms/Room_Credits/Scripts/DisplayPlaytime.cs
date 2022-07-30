using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DisplayPlaytime : MonoBehaviour
{
    private TMP_Text textField;
    
    // Start is called before the first frame update
    void Start()
    {
        // time in seconds
        double time = Time.realtimeSinceStartupAsDouble;
        int minutes = (int) (time / 60);
        int seconds = (int) ((time / 60.0f - minutes) * 60);
        
        textField = GetComponent<TMP_Text>();
        textField.text = minutes + " : " + seconds;
    }
}
