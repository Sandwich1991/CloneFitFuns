using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NickNameTooltip : MonoBehaviour
{
    [SerializeField] private Canvas _canvas;
    [SerializeField] private Text _nickname;
    private Camera _camera;

    private void Start()
    {
        _camera = Camera.main;
        
        _canvas.renderMode = RenderMode.WorldSpace;
        _canvas.worldCamera = _camera;

        _nickname.text = Managers.Player.Nickname;
    }

    private void Update()
    {
        _canvas.transform.rotation = _camera.transform.rotation;
    }
}
