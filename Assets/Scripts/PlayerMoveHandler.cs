using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerMoveHandler : Singleton<PlayerMoveHandler>
{
    [SerializeField] private Rigidbody2D _player;
    void Update()
    {
        if ((PlayerManager.instance.currentState == PlayerManager.PlayerState.Attack)
            || (PlayerManager.instance.currentState ==  PlayerManager.PlayerState.Idle))
        {
            _player.velocity = new Vector2(0, _player.velocity.y);
        }
        
        
        Vector2 __newVelocity = new Vector2(PlayerManager.instance.playerMoveVelocity.x, _player.velocity.y);
        _player.velocity = __newVelocity;

        if (PlayerManager.instance.currentState == PlayerManager.PlayerState.Jump)
        {
            Debug.Log("점프함");
            _player.AddForce(new Vector2(0, PlayerManager.instance.jumpPower));
        }
        
        
    }
}
