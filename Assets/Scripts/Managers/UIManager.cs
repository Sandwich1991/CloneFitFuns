using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class UIManager
{
    public void ConfirmWindow(string text, Transform parent, bool closeParent = false)
    {
        ConfirmWindow window = Managers.Resource.Instantiate("ConfirmWindow", parent).GetComponent<ConfirmWindow>();
        window.Text = text;
        
        window.confirmButton.onClick.AddListener(() =>
        {
            Managers.Resource.Destroy(window.gameObject);

            if (closeParent)
            {
                Managers.Resource.Destroy(parent.gameObject);
            }
        });
    }

    public void WarningWindow(string text, Transform parent, bool closeParent, Action confirm)
    {
        WarningWindow window = Managers.Resource.Instantiate("WarningWindow", parent).GetComponent<WarningWindow>();
        window.Text = text;
        
        window.confirmButton.onClick.AddListener(() =>
        {
            Managers.Resource.Destroy(window.gameObject);
            confirm.Invoke();
        });
        
        window.cancelButton.onClick.AddListener(() =>
        {
            Managers.Resource.Destroy(window.gameObject);

            if (closeParent)
            {
                Managers.Resource.Destroy(parent.gameObject);
            }
        });
    }
}
