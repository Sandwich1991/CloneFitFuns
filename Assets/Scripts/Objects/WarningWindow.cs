using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WarningWindow : MonoBehaviour
{
    public Button confirmButton;
    public Button cancelButton;
    [SerializeField] private Text text;
    public string Text
    {
        get => text.text;
        set => text.text = value;
    }
}
