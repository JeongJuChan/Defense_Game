using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimationEvent : AnimationEventHandler
{
    Enemy _enemy;
    Animator _anim;
    void Start()
    {
        _enemy = GetComponentInParent<Enemy>();
        _anim = GetComponent<Animator>();
    }

    void Update()
    {
        
    }

    protected override void OnAttackEvent()
    {
        
    }

    protected override void OnDamagedEvent()
    {
        _anim.SetInteger(_enemy.HitAnim, 0); 
    }
}
