using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ScriptableTorretta : ScriptableObject
{
    [Header("Stats")]
    public float raggio;
    public float danno;
    public float fire_rate;

    public virtual void Shoot(Transform target, Transform torretta, float actualDamage, float actualFire_rate) { }
}
