using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : LivingEntity, IDamageable
{
    [SerializeField] float blinkTimer = 1f;
    [SerializeField] float blinkTime = 0;
    GameObject _meshGameObject;
    bool _isInvincible;
    void Start()
    {
        Init();
    }
    
    void Init()
    {
        _meshGameObject = GameObject.Find("Mesh");
        blinkTime = blinkTimer;
    }

    void Update()
    {
        Blink();
    }

    public void Damage(int amount)
    {
        //base.Damage(amount);
        _isInvincible = true;
    }
    
    // TODO : 메쉬 깜빡이는 걸로 바꾸기
    void Blink()
    {
        if (!_isInvincible)
            return;
        if (blinkTime >= 0)
        {
            blinkTime -= Time.deltaTime;
            if (_meshGameObject.activeInHierarchy)
                _meshGameObject.SetActive(false);
        }
        else
        {
            blinkTime = blinkTimer;
            _meshGameObject.SetActive(true);
            _isInvincible = false;
        }            
    }
}
