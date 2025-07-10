using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerManager : Singleton<PlayerManager>
{
    [SerializeField] public float attackRangeHigh { get; private set; } = 1;
    public float attackRangeWidth { get; private set; } = 1;
    public float moveSpeed { get; private set; } = 3;
    public float jumpPower { get; private set; } = 1000;

    [SerializeField] private Rigidbody2D _player;
    
    public enum PlayerState
    {
          Idle
        , Move
        , Attack
        , Jump
        , Hit
    }

    [SerializeField] private bool canJump = true;

    public Vector2 playerMoveVelocity { get; private set; }
    public PlayerState currentState { get; private set; }  = PlayerState.Idle;


    void Update()
    {
        // 좌, 우 이동
        if (Input.GetKey(KeyCode.A)|| Input.GetKey(KeyCode.D))
        {
            playerMoveVelocity = new Vector2((Input.GetKey(KeyCode.D) ? 1 : -1) * moveSpeed, playerMoveVelocity.y) ;

            if (currentState == PlayerState.Jump) return;
            currentState = PlayerState.Move;
        }
        
        // 점프
        else if (Input.GetKeyDown(KeyCode.Space))
        {
            if (!canJump) return;
            Debug.Log("잠프 인식함");
            currentState = PlayerState.Jump;
            canJump = false;
        }
        
        // 공격
        else if (Input.GetMouseButtonDown(0))
        {
            if (currentState == PlayerState.Jump) return;
            currentState = PlayerState.Attack;
        }
        
        else
        {
            playerMoveVelocity = Vector2.zero;
            currentState = PlayerState.Idle;
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("ground"))
        {
            Debug.Log("점프 할 수 있음");
            canJump = true;
        }
    }
}
