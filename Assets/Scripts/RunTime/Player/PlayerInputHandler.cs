using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInputHandler : Singleton<PlayerInputHandler>
{

    public event Action OnIdleEvent;
    public event Action<int> OnMoveEvent;
    public event Action OnJumpEvent;
    public event Action OnAttackEvent;
    
    private void Update()
    {
        // 공격
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("공격함");
            OnAttackEvent.Invoke();
            return;
        }
        
        // 점프
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("점프함");
            OnJumpEvent.Invoke();
            return;
        }
        
        // 좌, 우 이동
        if (Input.GetKey(KeyCode.A)|| Input.GetKey(KeyCode.D))
        {
            // Debug.Log("달리기 함");
            OnMoveEvent.Invoke(Input.GetKey(KeyCode.A)? -1 : 1);
            return;
        }
        
        // Debug.Log("대기함");
        OnIdleEvent?.Invoke();
        
        
        
    }
    
}
