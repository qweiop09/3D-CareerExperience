using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class PlayerManager : Entity
{
    [Header("Player Manager")]
    [Space(10)]
    
    [SerializeField] private Rigidbody2D _player;
    [SerializeField] private Animator _animator;
    
    [SerializeField] private Collider2D _attackDamageCollider;
    private Vector3? _reSponePosition;
    
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
    
    [Header("Player Status")]
    [Space(10)]
    
    [SerializeField] private float _moveSpeed = 3;
    [SerializeField] private float _jumpPower = 650;
    
    [Header("Player State")]
    [Space(10)]
    
    [SerializeField] private PlayerState _currentState  = PlayerState.Idle;
    private Vector2 _playerMoveVelocity;
    
    [SerializeField] private bool _canJump = true;
    [SerializeField] private bool _isHit = false;
    [SerializeField] private bool _isWallSticking = false;

    void Start()
    {
        PlayerInputHandler.instance.OnAttackEvent += OnAttackEvent;
        PlayerInputHandler.instance.OnJumpEvent += OnJumpEvent;
        PlayerInputHandler.instance.OnMoveEvent += OnMoveEvent;
        PlayerInputHandler.instance.OnIdleEvent += OnIdleEvent;

        SensingIsGroundToPlayer.sensedIsGround += randing;
        
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
                _animator.Play("jump");
            }
            else
            {
                _animator.Play("Fall");
            }
        }
        
        if ( _currentState == PlayerState.Move)
        {
            RaycastHit2D[] hits = Physics2D.RaycastAll(transform.position, -_player.transform.up, 1.2f);
            foreach (var hit in hits)
            {
                if (hit.transform.CompareTag("ground"))
                {
                    _isWallSticking = false;
                    return;
                }
            }
            
            hits = Physics2D.RaycastAll(transform.position, _player.transform.right, 0.8f);
            foreach (var hit in hits)
            {
                if (hit.transform.CompareTag("ground"))
                {
                    _isWallSticking = true;
                    return;
                }
            }

            _isWallSticking = false;
            
        }
        
    }

    private void randing()
    {
        if (_currentState == PlayerState.Death) return;
        if (_currentState == PlayerState.Hit) return;
        if (_currentState != PlayerState.Jump) return;
        
        _currentState = PlayerState.Idle;
        _animator.Play("Idle");
        _canJump = true;
        _isWallSticking = false;
    }

    void OnAttackEvent()
    {
        if (_currentState == PlayerState.Death) return;
        if (_currentState == PlayerState.Hit) return;
        if (_currentState == PlayerState.Jump) return;
        
        _player.velocity = new Vector2(0, _player.velocity.y);
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
        
        _player.velocity = new Vector2(0, _player.velocity.y);
        _currentState = PlayerState.Jump;
        _canJump = false;

        PlayerMoveHandler.instance.ApplyPhysicsJump(_jumpPower);
        
        _animator.Play("jump");
    }

    void OnMoveEvent(int __direction)
    {
        if (_currentState == PlayerState.Death) return;
        if (_currentState == PlayerState.Hit) return;
        if (_currentState == PlayerState.Attack) return;
        if (_isWallSticking)
        {
            _player.velocity = new Vector2(0, _player.velocity.y);
            return;
        }
        
        _playerMoveVelocity = new Vector2(__direction * _moveSpeed, 0);
        PlayerMoveHandler.instance.ApplyPhysicsMove(__direction * _moveSpeed);
        
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

        PlayerMoveHandler.instance.ApplyPhysicsIdle();
        _animator.Play("Idle");
    }
    
    public override void OnHitEvent(int damage)
    {
        if (_currentState == PlayerState.Death) return;
        if (_currentState == PlayerState.Hit) return;
        
        _player.velocity = new Vector2(0, _player.velocity.y);
        
        _animator.Play("Hurt");
        _currentState = PlayerState.Hit;

         currentHp -= damage;
        _isHit = true;

        SetHPVar();

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

        SetHPVar();
        HPVar.instance.SetSecondHPVar();

        _animator.Play("Idle");
        transform.position = (Vector3)_reSponePosition;
        
        ReStart.Invoke();
    }

    public void SetStateIdle()
    {
        _currentState = PlayerState.Idle;
        
        _attackDamageCollider.enabled = false;
        _canJump = true;
    }

    private void SetHPVar()
    {
        HPVar.instance.SetHPVar( (float) currentHp / maxHp);
    }
    


}
