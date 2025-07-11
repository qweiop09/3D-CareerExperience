using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : Entity
{
    [SerializeField] protected int maxHp = 1;
     protected int currentHp;
    [SerializeField] protected float moveSpeed;

    protected void Awake()
    {
        currentHp = maxHp;
    }

    public abstract void Move();
    
}
