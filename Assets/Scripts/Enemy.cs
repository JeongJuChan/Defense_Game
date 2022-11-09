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
        if (collision.collider.CompareTag("Player"))
            collision.collider.GetComponent<IDamageable>().Damage(damage);
    }

    public void Damage(int amount)
    {
        health -= amount;

        // �������� ���� �ǰ� �ִϸ��̼� ó��
        // �ִϸ����� ���� ��� or . . ����ó�� 


        //10 �̸��� �������� �Ծ����� 
        if(amount <= 10) {

            Debug.Log(amount +"�� �������� ���� !!");
            anim.SetTrigger("LowHit");
        }

        //10~19 ������ �������� �Ծ�����
        if(amount > 10 && amount <= 20)
        {
            Debug.Log(amount + "�� �������� ���� !!");
            anim.SetTrigger("MiddleHit");
        }

        // 20~29 ������ �������� �Ծ�����
        if (amount > 20)
        {
            Debug.Log(amount + "�� �������� ���� !!");
            anim.SetTrigger("HighHit");
        }

    }
}
