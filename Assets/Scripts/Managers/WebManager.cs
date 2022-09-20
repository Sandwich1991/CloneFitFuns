using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WebManager
{
    public Action RefreshAction;

    public void PostChanged()
    {
        RefreshAction.Invoke();
    }
}
