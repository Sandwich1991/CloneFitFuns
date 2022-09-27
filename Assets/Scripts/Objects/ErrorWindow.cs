using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ErrorWindow : MonoBehaviour
{
    [SerializeField] private Text _errorMessage;
    
    public string Text
    {
        get { return _errorMessage.text; }
        set { _errorMessage.text = value; }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Managers.Resource.Destroy(gameObject);
        }
    }
}
