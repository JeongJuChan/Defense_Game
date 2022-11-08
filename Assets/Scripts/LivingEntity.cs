using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LivingEntity : MonoBehaviour
{
    [SerializeField] protected int damage = 10;
    [SerializeField] protected int health = 10;
    protected Animator anim;
    public int Health { get => health; set => health = value; }


}