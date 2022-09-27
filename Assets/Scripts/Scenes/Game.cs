using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{
    private const string MainUIPath = "MainUI";
    private const string BGMPath = "Sounds/BGM_03";
    private static GameObject _mainUI;
    private GameObject _player;
    private CameraController _cameraController;
    private Camera _camera;
    
    public static Transform MainUI => _mainUI.transform;

    [SerializeField] private Vector3 playerPos = new Vector3(0, 0, -30);
    
    private void Start()
    {
        _mainUI = Managers.Resource.Instantiate(MainUIPath);
        _player = Managers.Player.GeneratePlayer(playerPos);
        _camera = Camera.main;
        _cameraController = _camera.gameObject.AddComponent<CameraController>();

        Managers.Sound.Play(BGMPath, Define.SoundType.Bgm, 1.0f);
    }
}
