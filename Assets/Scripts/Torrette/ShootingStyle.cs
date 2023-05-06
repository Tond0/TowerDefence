using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ColpoSingolo : IShoot
{
    public GameObject proiettile { get; }
    
    private Transform torretta;
    public ColpoSingolo(GameObject proiettile, Transform torretta)
    {
        this.proiettile = proiettile;
        this.torretta = torretta;
    }

    private float timeRemaining;

    public void Shoot(Transform target, float fireRate, float danno)
    {
        if (timeRemaining > 0)
        {
            timeRemaining -= Time.deltaTime;
        }
        //Shoot
        else
        {
            #region Sparo
            torretta.LookAt(target.position);

            target.TryGetComponent<Entity>(out Entity enemy);
            enemy.PV -= danno;

            #endregion

            timeRemaining = fireRate;
        }
    }
}

public class Area : IShoot
{
    public GameObject proiettile { get; private set; }

    public void Shoot(Transform target, float fireRate, float danno)
    {
        throw new System.NotImplementedException();
    }
}

public class Mitragliatrice : IShoot
{
    public GameObject proiettile { get; private set; }

    public void Shoot(Transform target, float fireRate, float danno)
    {

    }
}