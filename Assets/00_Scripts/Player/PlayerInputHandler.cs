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
        // 공격 입력 인식
        if (Input.GetMouseButtonDown(0))
        {
            OnAttackEvent.Invoke();
            return;
        }
        
        // 점프 입력 인식
        if (Input.GetKeyDown(KeyCode.Space))
        {
            OnJumpEvent.Invoke();
            return;
        }
        
        // 측면 이동 입력 인식
        if (Input.GetKey(KeyCode.A)|| Input.GetKey(KeyCode.D))
        {
            OnMoveEvent.Invoke(Input.GetKey(KeyCode.A)? -1 : 1);
            return;
        }
        
        // 아무것도 아니면 대기상태로 전환
        OnIdleEvent?.Invoke();
        
        
        
    }
    
}
