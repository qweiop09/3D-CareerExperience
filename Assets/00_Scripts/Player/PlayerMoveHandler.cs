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
        _player.velocity = new Vector2(_player.velocity.x, 0);
        _player.AddForce(new Vector2(0, __jumpPower));
        
    }

    public void ApplyPhysicsMove(float __direction)
    {
        // 이동 코드
        Vector2 __newVelocity = new Vector2(__direction, _player.velocity.y);
        _player.velocity = __newVelocity;
        
        // 회전 코드
        _player.transform.rotation = Quaternion.Euler(new Vector3(0,_player.velocity.x < 0? 180 : 0,0));  
        
    }

    public void ApplyPhysicsIdle()
    {
        // 정지 코드
        _player.velocity = new Vector2(0, _player.velocity.y);
    }
    
    
}
