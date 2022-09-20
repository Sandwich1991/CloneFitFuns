using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WarningWindow : MonoBehaviour
{
    public Button ConfirmButton;
    public Button CancleButton;
    [SerializeField] private Text _text;
    public string Text
    {
        get { return _text.text; }
        set { _text.text = value; }
    }

    private void Start()
    {
        CancleButton.onClick.AddListener(() => Managers.Resource.Destroy(gameObject));
    }
}
