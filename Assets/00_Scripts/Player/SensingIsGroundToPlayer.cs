using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SensingIsGroundToPlayer : MonoBehaviour
{
    public static event Action sensedIsGround;
    
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("ground"))
        {
            sensedIsGround.Invoke();
        }
    }
}
