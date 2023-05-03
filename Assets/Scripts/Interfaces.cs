using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ITurret
{
    public GameObject proiettile { get; set; }

    public Color coloreTorretta { get; set; }

    public void Shoot();
}
