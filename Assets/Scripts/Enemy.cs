using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : LivingEntity, IDamageable
{
    Rigidbody rigid;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
    }


    void OnCollisionEnter(Collision collision)
    {
        collision.collider.GetComponent<IDamageable>().Damage(damage);

    }

    public void Damage(int amount)
    {
        health -= amount;

        // 데미지에 따른 피격 애니메이션 처리
        // 애니메이터 블랜더 사용 or . . 조건처리 


        //10 미만의 데미지를 입었을때 
        if(amount < 10) {

            Debug.Log(amount +"의 데미지를 입음 !!");
            anim.SetTrigger("LowHit");
        }

        //10~19 사이의 데미지를 입었을때
        if(amount >= 10 && amount < 20)
        {
            Debug.Log(amount + "의 데미지를 입음 !!");
            anim.SetTrigger("MiddleHit");
        }

        // 20~29 사이의 데미지를 입었을때
        if (amount >= 20 && amount < 30)
        {
            Debug.Log(amount + "의 데미지를 입음 !!");
            anim.SetTrigger("HighHit");
        }

    }
}
