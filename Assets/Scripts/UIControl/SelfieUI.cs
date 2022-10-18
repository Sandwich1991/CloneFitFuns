using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelfieUI : MonoBehaviour
{
    [SerializeField] private Button _recordButton;
    
    public Button exitButton;

    private void Awake()
    {
        _recordButton.onClick.AddListener(Capture);
    }

    void Capture()
    {
        // ScreenCapture capture 
    }
}
