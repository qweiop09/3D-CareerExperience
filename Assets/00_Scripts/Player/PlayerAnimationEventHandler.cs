using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationEventHandler : MonoBehaviour
{
    [SerializeField] private PlayerManager _playerManager;
    
    public void EndHit()
    {
        _playerManager.EndHit();
    }
    
    public void SetStateIdle()
    {
        _playerManager.SetStateIdle();
    }
    
    public void OnAttackDamageCollider()
    {
       _playerManager.OnAttackDamageCollider();
    }

    public void ReSpone()
    {
        _playerManager.ReSpone();
    }
}
