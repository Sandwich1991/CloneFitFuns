using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager
{
    private GameObject _player;
    private PlayerController _controller;

    public int Trash { get; set; }

    public Transform PlayerTransform => _player.transform;

    public string Nickname { get; set; }

    public Vector3 PlayerPos
    {
        get => _player.transform.position;
        set => _player.transform.position = value;
    }
    
    public Vector3 PlayerRot => _player.transform.eulerAngles;

    public GameObject GeneratePlayer(Vector3 pos)
    {
        _player = Managers.Resource.Instantiate("Character");
        PlayerPos = pos;
        
        _controller = _player.GetComponent<PlayerController>();
        if (_controller == null)
        {
            _player.AddComponent<PlayerController>();
            _controller = _player.GetComponent<PlayerController>();
        }

        _player.name = Nickname;
        
        return _player;
    }

    public void DisableMove()
    {
        Object.Destroy(_controller);
    }

    public void EnableMove()
    {
        _controller = _player.AddComponent<PlayerController>();
    }
}
