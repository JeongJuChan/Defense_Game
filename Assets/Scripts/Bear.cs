using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class Bear : LivingEntity, IDamageable
{
    public int Damage { get; set; }
    public int Health { get; set; }

    
    protected override void Init()
    {
        pv = GetComponent<PhotonView>();
        anim = GetComponentInChildren<Animator>();
        rigidbody = GetComponent<Rigidbody>();
        Health = 100;
        Damage = 10;
    }

    protected override void AnimInit()
    {
    }
    
    void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Player"))
            collision.collider.GetComponent<IDamageable>().Damaged(Damage);
    }

    public void Damaged(int amount)
    {
        Health -= amount;
    }

    
    
}
