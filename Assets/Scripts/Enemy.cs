using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class Enemy : LivingEntity, IDamageable
{
    public int Damage { get; set; }
    public int Health { get; set; }

    public int HitAnim { get; private set; }

    protected override void Init()
    {
        pv = GetComponent<PhotonView>();
        anim = GetComponentInChildren<Animator>();
        rigidbody = GetComponent<Rigidbody>();
        Health = 50;
        Damage = 5;
    }

    protected override void AnimInit()
    {
        HitAnim = Animator.StringToHash("hitAnim");
    }
    
    void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Player"))
            collision.collider.GetComponent<IDamageable>().Damaged(Damage);
    }

    public void Damaged(int amount)
    {
        Debug.Log(amount);
        Health -= amount;
        anim.SetInteger(HitAnim, amount);
    }
    
   
}
