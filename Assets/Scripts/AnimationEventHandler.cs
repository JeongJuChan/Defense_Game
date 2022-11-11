using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AnimationEventHandler : MonoBehaviour
{
    public Action<int> AttackEvent;
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

    void OnAttackEvent()
    {
        AttackEvent?.Invoke(_player.Damage);
    }
}
