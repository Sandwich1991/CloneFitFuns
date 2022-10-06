using System;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Define.PlayerState _playerState;
    private Animator _animator;
    private Vector3 _desPos;
    private Camera _camera;
    private readonly int _moveBlockMask = (1 << (int)Define.Layer.Block) | (1 << (int)Define.Layer.Clickable);
    
    private const string PathStepSound = "Sounds/Walk";
    
    [SerializeField] private float walkSpeed = 5f;
    
    
    void UpdateIdle()
    {
        _animator.Play("Idle");
    }

    void UpdateWalk()
    {
        Vector3 dir = _desPos - transform.position;
        
        if (dir.magnitude < 0.1f)
            _playerState = Define.PlayerState.Idle;
        
        else 
        {
            if (Physics.Raycast(transform.position + Vector3.up * 0.3f, dir, 1f, _moveBlockMask))
            {
                if (Input.GetMouseButton(0) == false)
                    _playerState = Define.PlayerState.Idle;
                return;
            }
            
            float distToMove = Math.Clamp(walkSpeed * Time.deltaTime, 0, dir.magnitude);
            transform.position += dir.normalized * distToMove;
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(dir), 10 * Time.deltaTime);
            
            _animator.Play("Walk");
        }
    }

    void OnMouse(Define.MouseEvent @event)
    {
        RaycastHit hit;
        Ray ray = _camera.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit, 100f, LayerMask.GetMask("Ground")))
        {
            _desPos = hit.point;
            _playerState = Define.PlayerState.Walk;
        }
    }
    
    public void FootStep()
    {
        Managers.Sound.Play(PathStepSound, Define.SoundType.Effect, 1.0f);
    }
    
    private void Start()
    {
        _camera = Camera.main;
        _animator = GetComponent<Animator>();
        
        Managers.Input.MouseAction -= OnMouse;
        Managers.Input.MouseAction += OnMouse;
    }

    private void Update()
    {
        switch (_playerState)
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
