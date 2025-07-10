using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerAnimationHandler : Singleton<PlayerAnimationHandler>
{
    public void Update()
    {
        PlayerManager.PlayerState __currentState = PlayerManager.instance.currentState;
        if (__currentState == PlayerManager.PlayerState.Idle)
        {
            
        }
    }
}
