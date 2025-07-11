using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerManager : Entity
{
    
    [SerializeField] private Rigidbody2D _player;
    [SerializeField] private Animator _animator;
    
    [SerializeField] private float attackRangeHigh = 1;
    [SerializeField] private float attackRangeWidth = 1;
    [SerializeField] private float moveSpeed = 3;
    [SerializeField] private float jumpPower = 650;
    
    public enum PlayerState
    {
          Idle
        , Move
        , Attack
        , Jump
        , Hit
    }
    
    [SerializeField] private Vector2 playerMoveVelocity;
    [SerializeField] private PlayerState currentState  = PlayerState.Idle;

    [SerializeField] public bool canJump = true;

    void Start()
    {
        Debug.Log("널음1");
        PlayerInputHandler.instance.OnAttackEvent += OnAttackEvent;
        PlayerInputHandler.instance.OnJumpEvent += OnJumpEvent;
        PlayerInputHandler.instance.OnMoveEvent += OnMoveEvent;
        PlayerInputHandler.instance.OnIdleEvent += OnIdleEvent;

        SensingIsGroundToPlayer.sensedIsGround += randing;
        
        Debug.Log("널음");
    }
    
    
    void Update()
    {
        
        if (currentState == PlayerState.Jump)
        {
            canJump = false;
            currentState = PlayerState.Jump;
            if (_player.velocity.y > 0)
            {
                Debug.Log("점프 중");
                _animator.Play("jump");
            }
            else
            {
                Debug.Log("떨어지는 중!");
                _animator.Play("Fall");
                
                // RaycastHit2D[] hits = Physics2D.RaycastAll(transform.position, new Vector2(0, -1), 0.7f);
                // Debug.DrawRay(transform.position, new Vector2(0,-1) * 0.7f, Color.red, 1f);
                //
                // foreach (RaycastHit2D hit in hits)
                // {
                //     if (hit.transform.CompareTag("ground"))
                //     {
                //         Debug.Log("착지함");
                //         Debug.Log(Time.frameCount);
                //
                //         currentState = PlayerState.Idle;
                //         _animator.Play("Idle");
                //         canJump = true;
                //     }
                // }
            }
            
        }
        
    }

    private void randing()
    {
        if (currentState != PlayerState.Jump) return;
        
        Debug.Log("점프 할 수 있게 됨");
        Debug.Log(Time.frameCount);
        
        currentState = PlayerState.Idle;
        _animator.Play("Idle");
        canJump = true;
    }

    void OnAttackEvent()
    {
        currentState = PlayerState.Attack;
        
        _animator.Play("Dash-Attack");
    }

    void OnJumpEvent()
    {
        if (currentState == PlayerState.Attack) return;
        if (!canJump) return;
        
        currentState = PlayerState.Jump;
        canJump = false;

        PlayerMoveHandler.instance.PhysicsToJump(jumpPower);
        
        _animator.Play("jump");
    }

    void OnMoveEvent(int __direction)
    {
        if (currentState == PlayerState.Attack) return;
        
        playerMoveVelocity = new Vector2(__direction * moveSpeed, 0);
        PlayerMoveHandler.instance.PhysicsToMove(__direction * moveSpeed);
        
        if (currentState == PlayerState.Jump) return;
        
         currentState = PlayerState.Move;
        _animator.Play("Run");
    }

    void OnIdleEvent()
    {
        if (currentState != PlayerState.Move) return;
        
        currentState = PlayerState.Idle;
        canJump = true;

        PlayerMoveHandler.instance.PhysicsToIdle();
        _animator.Play("Idle");
    }
    
    public override void OnHitEvent()
    {
        
    }

    public void SetStateIdle()
    {
        Debug.Log("SetStateIdle");
        currentState = PlayerState.Idle;
        canJump = true;
    }
    


}
