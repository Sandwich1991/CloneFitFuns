using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{
    private GameObject _mainUI;
    private string _mainUIPath = "MainUI";
    
    private GameObject _player;
    [SerializeField] private Vector3 _playerPos = new Vector3(0, 0, -30);
    
    private Camera _camera;
    private CameraController _cameraController;

    private string _bgmPath = "Sounds/BGM_03";

    private void Start()
    {
        _mainUI = Managers.Resource.Instantiate(_mainUIPath);
        
        _player = Managers.Player.GeneratePlayer(_playerPos);
        
        _camera = Camera.main;
        _cameraController = _camera.gameObject.AddComponent<CameraController>();
        _cameraController._player = _player;
        
        Managers.Sound.Play(_bgmPath, Define.SoundType.Bgm, 1.0f);
    }
}
