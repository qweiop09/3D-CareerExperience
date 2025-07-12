using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerMoveHandler : Singleton<PlayerMoveHandler>
{
    [SerializeField] private Rigidbody2D _player;
    
    public void ApplyPhysicsJump(float __jumpPower)
    {
        // 점프 코드
        
        
        
    }

    public void ApplyPhysicsMove(float __direction)
    {
        // 이동 코드
        
        
        
        // 회전 코드
        
        
        
    }

    public void ApplyPhysicsIdle()
    {
        // 정지 코드
        
        
        
    }
    
    
}
