using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IShoot
{
    public GameObject proiettile { get; }

    public void Shoot(Transform target, float fireRate, float danno);
}
