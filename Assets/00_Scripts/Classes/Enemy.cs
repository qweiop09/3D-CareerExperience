using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : Entity
{
    // [T]
    
    [SerializeField] protected float _moveSpeed;
    protected bool _isHit = false;
    protected bool _isDead = false;

    protected void Awake()
    {
        base.Awake();
    }

    public abstract void Move();
    
}
