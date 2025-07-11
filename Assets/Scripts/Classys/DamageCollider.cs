using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageCollider : MonoBehaviour
{
    [SerializeField] private int _damage;

    private void OnTriggerEnter2D(Collider2D __other)
    {
        Entity __otherEntity = __other.GetComponent<Entity>();
        
        if (__otherEntity != null)
        {
            if (transform.CompareTag(__other.tag))
            {
                return;
            }
            
            __otherEntity.OnHitEvent(_damage);
        }
    }
}
