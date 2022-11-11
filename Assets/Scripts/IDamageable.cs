using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamageable
{
    int Damage { get; set; }
    int Health { get; set; }
    void Damaged(int amount);
}
