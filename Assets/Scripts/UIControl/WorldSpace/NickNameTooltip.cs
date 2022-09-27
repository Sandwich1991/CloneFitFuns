using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NickNameTooltip : MonoBehaviour
{
    private Canvas _canvas;
    [SerializeField] private Text nickname;
    private Camera _camera;

    private void Start()
    {
        _camera = Camera.main;
        _canvas = GetComponent<Canvas>();
        _canvas.renderMode = RenderMode.WorldSpace;
        _canvas.worldCamera = _camera;
        nickname.text = Managers.Player.Nickname;
    }

    private void Update()
    {
        _canvas.transform.rotation = _camera.transform.rotation;
    }
}
