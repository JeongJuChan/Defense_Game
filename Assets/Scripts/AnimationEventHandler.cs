using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public abstract class AnimationEventHandler : MonoBehaviour
{
    public Action<int> AttackEvent;

    protected abstract void OnAttackEvent();
    protected abstract void OnDamagedEvent();


}
