using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStat : MonoBehaviour, IDamageable
{
    public int Damage { get; set; }
    public int Health { get; set; }
    
    [SerializeField] float _blinkTimer = 1f;
    [SerializeField] float _blinkTime;
    
    GameObject _meshGameObject;
    bool _isInvincible;
    
    void Start()
    {
        Init();
    }
    
    void Init()
    {
        _meshGameObject = GameObject.Find("Mesh");
        _blinkTime = _blinkTimer;
    }

    void Update()
    {
        // TODO 포스트 프로세싱으로 바꾸기
        // Blink();
    }
    
    // TODO : 메쉬 깜빡이는 걸로 바꾸기
    void Blink()
    {
        if (!_isInvincible)
            return;
        if (_blinkTime >= 0)
        {
            _blinkTime -= Time.deltaTime;
            if (_meshGameObject.activeInHierarchy)
                _meshGameObject.SetActive(false);
        }
        else
        {
            _blinkTime = _blinkTimer;
            _meshGameObject.SetActive(true);
            _isInvincible = false;
        }            
    }

    public void Damaged(int amount)
    {
        Health -= amount;
        _isInvincible = true;
    }
    
    
}
