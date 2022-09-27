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
        
        window.ConfirmButton.onClick.AddListener(() =>
        {
            Managers.Resource.Destroy(window.gameObject);

            if (closeParent)
            {
                Managers.Resource.Destroy(parent.gameObject);
            }
        });
    }

    public void ErrorWindow(string text)
    {
        ErrorWindow window = Managers.Resource.Instantiate("ErrorWindow").GetComponent<ErrorWindow>();
        window.Text = text;
    }
}
