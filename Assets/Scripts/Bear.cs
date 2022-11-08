using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bear : LivingEntity, IDamageable
{
    public void Damage(int amount)
    {
        health -= amount;

        // 데미지에 따른 피격 애니메이션 처리

        Debug.Log(" 데미지 입음 !!");

    }

    void OnCollisionEnter(Collision collision)
    {
        collision.collider.GetComponent<IDamageable>().Damage(damage);
    }
}
