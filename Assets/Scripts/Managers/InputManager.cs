using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class InputManager
{
    // Fields
    public Action KeyAction;
    public Action<Define.MouseEvent> MouseAction;

    private bool _pressed;
    
    // Methods
    public void OnUpdate()
    {
        if (EventSystem.current.IsPointerOverGameObject())
        {
            return;
        }
        
        if (Input.anyKey && KeyAction != null)
        {
            KeyAction.Invoke();
        }

        if (MouseAction != null)
        {
            if (Input.GetMouseButton(0))
            {
                MouseAction.Invoke(Define.MouseEvent.Press);
                _pressed = true;
            }
            else if (_pressed)
            {
                MouseAction.Invoke(Define.MouseEvent.Click);
            }

            _pressed = false;
        }
    }

    public void Clear()
    {
        KeyAction = null;
        MouseAction = null;
    }
}
