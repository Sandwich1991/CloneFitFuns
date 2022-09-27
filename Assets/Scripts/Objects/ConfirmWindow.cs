using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ConfirmWindow : MonoBehaviour
{
    [SerializeField] private Text text;
    
    public Button confirmButton;
    public string Text
    {   
        get => text.text;
        set => text.text = value;
    }
}
