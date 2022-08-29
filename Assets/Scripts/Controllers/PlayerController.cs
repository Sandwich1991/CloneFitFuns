using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Fields
    private Define.PlayerState _playerState;
    private Animator _animator;
    private Vector3 _desPos;
    private Camera _camera;
    [SerializeField] private float _walkSpeed = 5f;

    private string _stepSoundPath = "Sounds/Walk";
    
    private int _moveBlockMask = (1 << (int)Define.Layer.Block) | (1 << (int)Define.Layer.EventObject);
    
    // Property
    Define.PlayerState PlayerState
    {
        get { return _playerState; }
        set
        {
            _playerState = value;
            switch (PlayerState)
            {
                case Define.PlayerState.Idle:
                    _animator.Play("Idle");
                    break;
                
                case Define.PlayerState.Walk:
                    _animator.Play("Walk");
                    break;
            }
        }
    }
    
    // Methods
    void UpdateIdle()
    {
        
    }

    void UpdateWalk()
    {
        Vector3 dir = _desPos - transform.position;
        
        if (dir.magnitude < 0.1f)
            PlayerState = Define.PlayerState.Idle;
        
        else 
        {
            if (Physics.Raycast(transform.position + Vector3.up * 0.3f, dir, 1f, _moveBlockMask))
            {
                if (Input.GetMouseButton(0) == false)
                    PlayerState = Define.PlayerState.Idle;
                return;
            }
            
            float distToMove = Math.Clamp(_walkSpeed * Time.deltaTime, 0, dir.magnitude);
            transform.position += dir.normalized * distToMove;
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(dir), 10 * Time.deltaTime);
        }
        
    }

    void OnMouse(Define.MouseEvent @event)
    {
        RaycastHit hit;
        Ray ray = _camera.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit, 100f, LayerMask.GetMask("Ground")))
        {
            _desPos = hit.point;
            PlayerState = Define.PlayerState.Walk;
        }
    }

    public void FootStep()
    {
        Managers.Sound.Play(_stepSoundPath, Define.SoundType.Effect, 1.0f);
    }

    // MonoBehaviour
    private void Start()
    {
        _camera = Camera.main;
        _animator = GetComponent<Animator>();
        
        Managers.Input.MouseAction -= OnMouse;
        Managers.Input.MouseAction += OnMouse;
    }

    private void Update()
    {
        // FMS
        switch (PlayerState)
        {
            case Define.PlayerState.Idle:
                UpdateIdle();
                break;
            case Define.PlayerState.Walk:
                UpdateWalk();
                break;
        }
        
        
    }
}
