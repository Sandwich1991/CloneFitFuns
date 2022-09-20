using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ConfirmWindow : MonoBehaviour
{
    public Button ConfirmButton;
    [SerializeField] private Text _text;
    public string Text
    {
        get { return _text.text; }
        set { _text.text = value; }
    }
}
