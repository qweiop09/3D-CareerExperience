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
            Debug.Log("전투진행 피격대상 : " + __other.name );
            __otherEntity.OnHitEvent(_damage);
        }
    }
}
