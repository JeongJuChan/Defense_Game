using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class Enemy : LivingEntity, IDamageable
{
    public int Damage { get; set; }
    public int Health { get; set; }

    int _hitAnim;

    protected override void Init()
    {
        pv = GetComponent<PhotonView>();
        animator = GetComponentInChildren<Animator>();
        rigidbody = GetComponent<Rigidbody>();
        Health = 50;
        Damage = 5;
    }

    protected override void AnimInit()
    {
        _hitAnim = Animator.StringToHash("hitAnim");
    }
    
    void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Player"))
            collision.collider.GetComponent<IDamageable>().Damaged(Damage);
    }

    public void Damaged(int amount)
    {
        Health -= amount;
        anim.SetInteger(_hitAnim, amount);
    }
    
   
}
