using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerMoveHandler : Singleton<PlayerMoveHandler>
{
    [SerializeField] private Rigidbody2D _player;


    public void PhysicsToJump(float __jumpPower)
    {
        // 점프 인식
        Debug.Log("점뿌!!!!");
        Debug.Log(Time.frameCount);
        _player.velocity = new Vector2(_player.velocity.x, 0);
        _player.AddForce(new Vector2(0, __jumpPower));
        
    }

    public void PhysicsToMove(float __direction)
    {
        // 이동 연산
        Vector2 __newVelocity = new Vector2(__direction, _player.velocity.y);
        _player.velocity = __newVelocity;
        
        // 회전 연산
        _player.transform.rotation = Quaternion.Euler(new Vector3(0,_player.velocity.x < 0? 180 : 0,0));  
        
    }

    public void PhysicsToIdle()
    {
        // 정지 인식
        _player.velocity = new Vector2(0, _player.velocity.y);
    }
            
        
        
        
        
        
       
        
        
        
        
        
    
}
