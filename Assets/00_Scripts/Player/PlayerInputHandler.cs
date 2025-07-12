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
            
            return;
        }
        
        // 점프 입력 인식
        if (Input.GetKeyDown(KeyCode.Space))
        {
            
            return;
        }
        
        // 측면 이동 입력 인식
        if (Input.GetKey(KeyCode.A)|| Input.GetKey(KeyCode.D))
        {
            
            return;
        }
        
        // 아무것도 상태도 아니게 되면 대기상태로 전환
        
        
        
    }
    
}
