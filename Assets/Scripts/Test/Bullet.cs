using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    float _speed = 5;
    Vector3 _direction;

    public int ID { get; private set; }
    public int OwnerID { get; private set; }
    public bool Equals(int id, int ownerId) => id == ID && ownerId == OwnerID;

    public void Init(int id, int ownerId, Vector3 dir)
    {
        ID = ID;
        OwnerID = ownerId;
        _direction = dir;
    }
    
    void Update()
    {
        transform.Translate(_direction * (_speed * Time.deltaTime));
    }

    void OnBecameInvisible()
    {
        Destroy(gameObject);
    }

    
}
