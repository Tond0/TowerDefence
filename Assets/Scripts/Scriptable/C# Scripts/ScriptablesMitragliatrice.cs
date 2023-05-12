using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Mitragliatrice", menuName = "Torrette/Mitragliatrice")]
public class ScriptablesMitragliatrice : ScriptableTorretta
{
    public float maxAmmo;
    public float reloadTime;

    private float timeRemainingShoot;
    private float timeRemainingReload;
    private float reaminingAmmo;
    public override void Shoot(Transform torretta, Transform target, float actualDamage, float actualFire_rate)
    {
        if (reaminingAmmo > 0)
        {
            if (timeRemainingShoot > 0)
            {
                timeRemainingShoot -= Time.deltaTime;
            }
            //Shoot
            else
            {
                #region Sparo
                torretta.LookAt(target.position);

                target.TryGetComponent<Entity>(out Entity enemy);
                enemy.PV -= actualDamage;

                #endregion

                timeRemainingShoot = actualFire_rate;
                reaminingAmmo--;
            }
        }
        else
        {
            if(timeRemainingReload> 0)
            {
                timeRemainingReload -= Time.deltaTime;
            }
            else
            {
                reaminingAmmo = maxAmmo;
                timeRemainingReload = reloadTime;
            }
        }
    }
}
