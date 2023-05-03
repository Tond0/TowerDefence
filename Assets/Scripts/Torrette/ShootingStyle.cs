using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ColpoSingolo : IShoot
{
    public GameObject proiettile { get; private set; }
    public Color coloreTorretta { get; private set; }

    public void Shoot()
    {

    }
}

public class Area : IShoot
{
    public GameObject proiettile { get; private set; }
    public Color coloreTorretta { get; private set; }

    public void Shoot()
    {

    }
}

public class Mitragliatrice : IShoot
{
    public GameObject proiettile { get; private set; }
    public Color coloreTorretta { get; private set; }

    public void Shoot()
    {

    }
}