using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    // Fields
    [SerializeField] private Camera _camera;
    [SerializeField] public GameObject _player;

    private Vector3 _deltaPos = new Vector3(0, 7, -5);

    private void Start()
    {
        _camera = Camera.main;
    }

    private void LateUpdate()
    {
        _camera.transform.position = _player.transform.position + _deltaPos;
        _camera.transform.LookAt(_player.transform);
    }
}
