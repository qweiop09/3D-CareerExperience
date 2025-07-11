using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Unity.VisualScripting;
using UnityEngine;

public class Mushroom : Enemy
{
    private Rigidbody2D _rigidbody;
    private Animator _animator;

    private int _direction = 1;

    private void Awake()
    {
        base.Awake();
        
        _rigidbody = transform.GetComponent<Rigidbody2D>();
        _animator = transform.GetComponent<Animator>();
        
    }

    private void Update()
    {
        Move();
    }


    public override void Move()
    {
        RaycastHit2D[] __rayHits = Physics2D.RaycastAll(transform.position - new Vector3(0,1f, 0), new Vector2(_direction, 0), 1.5f, ~(1 << LayerMask.NameToLayer("Entity")));
        Debug.DrawRay(transform.position - new Vector3(0,1f, 0), new Vector2(_direction * 1.5f, 0), Color.red);
        
        foreach (var ___rayHit  in __rayHits)
        {
            if ( ___rayHit != null)
            {
                Debug.Log("충돌한 것: " + ___rayHit.collider.name);
                
                _direction *= -1;
                
                break;
            }
        }
        
        _rigidbody.velocity = new Vector2(_direction * moveSpeed, 0);
        transform.rotation =  Quaternion.Euler(new Vector3(0, 90 - 90 * _direction, 0));

        _animator.Play("MushRoomMove");
    }
    
    public override void OnHitEvent()
    {
        
    }

    
}
