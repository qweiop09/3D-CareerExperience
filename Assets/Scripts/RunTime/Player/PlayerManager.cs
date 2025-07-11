using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;

public class PlayerManager : Entity
{
    [SerializeField] private Rigidbody2D _player;
    [SerializeField] private Animator _animator;
    
    [SerializeField] private float _attackRangeHigh = 1;
    [SerializeField] private float _attackRangeWidth = 1;
    [SerializeField] private float _moveSpeed = 3;
    [SerializeField] private float _jumpPower = 650;
    [SerializeField] private Vector3? _reSponePosition;
    
    [SerializeField] private Collider2D _attackDamageCollider;
    
    public enum PlayerState
    {
          Idle
        , Move
        , Attack
        , Jump
        , Hit
        , Death
    }

    public static event Action ReStart;
    
    [SerializeField] private Vector2 _playerMoveVelocity;
    [SerializeField] private PlayerState _currentState  = PlayerState.Idle;
    
    [SerializeField] private bool _canJump = true;
    [SerializeField] private bool _isHit = false;

    void Start()
    {
        Debug.Log("널음1");
        PlayerInputHandler.instance.OnAttackEvent += OnAttackEvent;
        PlayerInputHandler.instance.OnJumpEvent += OnJumpEvent;
        PlayerInputHandler.instance.OnMoveEvent += OnMoveEvent;
        PlayerInputHandler.instance.OnIdleEvent += OnIdleEvent;

        SensingIsGroundToPlayer.sensedIsGround += randing;
        
        Debug.Log("널음");

        if (_reSponePosition == null) _reSponePosition = _player.position;
    }
    
    
    void Update()
    {
        if (_currentState == PlayerState.Jump)
        {
            _canJump = false;
            _currentState = PlayerState.Jump;
            if (_player.velocity.y > 0)
            {
                Debug.Log("점프 중");
                _animator.Play("jump");
            }
            else
            {
                Debug.Log("떨어지는 중!");
                _animator.Play("Fall");
            }
            
        }
        
    }

    private void randing()
    {
        if (_currentState == PlayerState.Death) return;
        if (_currentState == PlayerState.Hit) return;
        if (_currentState != PlayerState.Jump) return;
        
        Debug.Log("점프 할 수 있게 됨");
        Debug.Log(Time.frameCount);
        
        _currentState = PlayerState.Idle;
        _animator.Play("Idle");
        _canJump = true;
    }

    void OnAttackEvent()
    {
        if (_currentState == PlayerState.Death) return;
        if (_currentState == PlayerState.Hit) return;
        if (_currentState == PlayerState.Jump) return;
        
        
        _currentState = PlayerState.Attack;
        
        _animator.Play("Dash-Attack");
    }

    public void OnAttackDamageCollider()
    {
        _attackDamageCollider.enabled = true;
    }

    void OnJumpEvent()
    {
        if (_currentState == PlayerState.Death) return;
        if (_currentState == PlayerState.Hit) return;
        if (_currentState == PlayerState.Attack) return;
        if (!_canJump) return;
        
        _currentState = PlayerState.Jump;
        _canJump = false;

        PlayerMoveHandler.instance.PhysicsToJump(_jumpPower);
        
        _animator.Play("jump");
    }

    void OnMoveEvent(int __direction)
    {
        if (_currentState == PlayerState.Death) return;
        if (_currentState == PlayerState.Hit) return;
        if (_currentState == PlayerState.Attack) return;
        
        _playerMoveVelocity = new Vector2(__direction * _moveSpeed, 0);
        PlayerMoveHandler.instance.PhysicsToMove(__direction * _moveSpeed);
        
        if (_currentState == PlayerState.Jump) return;
        
         _currentState = PlayerState.Move;
        _animator.Play("Run");
    }

    void OnIdleEvent()
    {
        if (_currentState == PlayerState.Death) return;
        if (_currentState == PlayerState.Hit) return;
        if (_currentState != PlayerState.Move) return;
        
        _currentState = PlayerState.Idle;
        _canJump = true;

        PlayerMoveHandler.instance.PhysicsToIdle();
        _animator.Play("Idle");
    }
    
    public override void OnHitEvent(int damage)
    {
        _animator.Play("Hurt");
        _currentState = PlayerState.Hit;

         currentHp -= damage;
        _isHit = true;

        if (currentHp <= 0)
        {
            _animator.Play("Death");
            _currentState = PlayerState.Death;
        }
    }

    public void EndHit()
    {
        _animator.Play("Idle");

        _currentState = PlayerState.Idle;
        _isHit = false;
    }

    public void ReSpone()
    {
        currentHp = maxHp;
        _currentState = PlayerState.Idle;

        _isHit = false;

        _animator.Play("Idle");
        transform.position = (Vector3)_reSponePosition;
        
        ReStart.Invoke();
    }

    public void SetStateIdle()
    {
        Debug.Log("SetStateIdle");
        _currentState = PlayerState.Idle;
        
        _attackDamageCollider.enabled = false;
        _canJump = true;
    }
    


}
