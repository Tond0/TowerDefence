using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IShoot
{
    public GameObject proiettile { get; }

    public Color coloreTorretta { get; }

    public void Shoot();
}
