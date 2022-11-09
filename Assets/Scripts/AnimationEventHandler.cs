using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AnimationEventHandler : MonoBehaviour
{
    public Action<int> AttackEvent;
    public Action EquipEvent;
    PlayerController player;

    int damage;

    private void Awake()
    {
        player = GetComponentInParent<PlayerController>();
    }
    
    void OnEquipEvent()
    {
        EquipEvent?.Invoke();
    }

    void OnAttackEvent()
    {
        damage = player._damage;
        AttackEvent?.Invoke(damage);
    }
}
