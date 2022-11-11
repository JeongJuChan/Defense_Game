using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public abstract class LivingEntity : MonoBehaviourPunCallbacks
{
    protected Animator anim;
    protected Rigidbody rigidbody;
    protected PhotonView pv;
    
    void Awake()
    {
        Init();
        AnimInit();
    }

    protected abstract void Init();
    protected abstract void AnimInit();



}