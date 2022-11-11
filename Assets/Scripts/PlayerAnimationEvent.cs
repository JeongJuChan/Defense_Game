using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationEvent : AnimationEventHandler
{
    public Action EquipEvent;
    
    PlayerStat _player;    

    void Awake()
    {
        _player = GetComponentInParent<PlayerStat>();
    }

    void OnEquipEvent()
    {
        EquipEvent?.Invoke();
    }

    protected override void OnAttackEvent()
    {
        AttackEvent?.Invoke(_player.Damage);
    }

    protected override void OnDamagedEvent()
    {
        
    }
}
