using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;

public class Mushroom : Enemy
{
    private Rigidbody2D _rigidbody;
    private Animator _animator;

    private SpriteRenderer _spriteRenderer;

    private Vector3 _reSponePosition;

    [SerializeField] private Collider2D _damageCollider;

    private int _direction = 1;

    private void Awake()
    {
        base.Awake();
        
        _rigidbody = transform.GetComponent<Rigidbody2D>();
        _animator = transform.GetComponent<Animator>();
        _spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        
        _reSponePosition = transform.position;

        PlayerManager.ReStart += ReSpone;

    }

    private void Update()
    {
        if (_isDead) return;
        
        Move();
    }

    public override void Move()
    {
        if (_isHit)
        {
            _rigidbody.velocity = Vector2.zero;

            return;
        }
        
        RaycastHit2D[] __rayHits = Physics2D.RaycastAll(
            transform.position - new Vector3(0, 1f, 0),
            new Vector2(_direction, 0),
            1.5f,
            ~(LayerMask.GetMask("Entity", "DamageCollider"))
        );
        
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
        
        _rigidbody.velocity = new Vector2(_direction * _moveSpeed, 0);
        transform.rotation =  Quaternion.Euler(new Vector3(0, 90 - 90 * _direction, 0));

        _animator.Play("MushRoomMove");
    }
    
    public override void OnHitEvent(int damage)
    {
        if (currentHp <= 0)
        {
            return;
        }
        
        Debug.Log("아얏");
        currentHp -= damage;

        _isHit = true;
        
        if (currentHp <= 0)
        {
            _animator.Play("MushRoomDeath");
            _damageCollider.enabled = false;

            _isDead = true;
            
            return;
        }
        
        _animator.Play("MushRoomHit");
        
        
    }

    public void OnDeath()
    {
        _spriteRenderer.enabled = false;
    }

    public void NotIsHit()
    {
        _isHit = false;
    }

    private void ReSpone()
    {
        transform.position = _reSponePosition;

        _spriteRenderer.enabled = true;
        _damageCollider.enabled = true;
        _isDead = false;
        _isHit = false;
        
        currentHp = maxHp;

        _direction = 1;

    }
    
}
