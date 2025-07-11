using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TrapTimeMapTakeDamage : MonoBehaviour
{
    private Collider2D _collider;
    
    private float _damageDelayTime = 0.45f;
    private float _elpsedTime = 0;

    private void Awake()
    {
        _collider = transform.GetComponent<Collider2D>();
    }

    private void Update()
    {
        if (!_collider.enabled) _collider.enabled = true;
    }

    private void OnTriggerEnter2D(Collider2D __other)
    {
        _elpsedTime = 0;
        Debug.Log("안녕!!");
    }

    private void OnTriggerStay2D(Collider2D __other)
    {
        Entity __otherEntity = __other.GetComponent<Entity>();
        
        if (__otherEntity != null)
        {
            if (transform.CompareTag(__other.tag)) return;

            _elpsedTime += Time.deltaTime;
            Debug.Log(_elpsedTime);

            if (_damageDelayTime <= _elpsedTime)
            {
                Debug.Log("데미지 다시 입힌다!!");
                _collider.enabled = false;
                _elpsedTime = 0;
            }

        }

        else
        {
            _elpsedTime = 0;
        }
    }
}
