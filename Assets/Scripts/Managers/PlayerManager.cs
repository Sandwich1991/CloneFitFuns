using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager
{
    public GameObject _player;
    public PlayerController _controller;
    private Rigidbody _rigidbody;
    private string _nickname;
    public string Nickname
    {
        get { return _nickname; }
        set { _nickname = value; }
    }
    
    public GameObject GeneratePlayer(Vector3 pos)
    {
        _player = Managers.Resource.Instantiate("Character");
        _player.transform.position = pos;
        
        _controller = _player.GetComponent<PlayerController>();
        if (_controller == null)
        {
            _player.AddComponent<PlayerController>();
            _controller = _player.GetComponent<PlayerController>();
        }

        _rigidbody = _player.GetComponent<Rigidbody>();
        if (_rigidbody == null)
        {
            _player.AddComponent<Rigidbody>();
            _rigidbody = _player.GetComponent<Rigidbody>();
        }
        _rigidbody.constraints = RigidbodyConstraints.FreezeRotation;

        _player.name = Nickname;
        
        return _player;
    }
}
