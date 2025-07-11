using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Entity : MonoBehaviour
{
    [SerializeField] protected int maxHp = 1;
    [SerializeField] protected int currentHp;
    
    protected void Awake()
    {
        currentHp = maxHp;
    }
    
    public abstract void OnHitEvent(int damage);
}
