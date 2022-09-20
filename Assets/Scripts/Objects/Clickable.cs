using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public abstract class Clickable : MonoBehaviour
{
    protected abstract void init();

    public abstract void OnMouseDown();

    protected void Start()
    {
        init();
    }
}
