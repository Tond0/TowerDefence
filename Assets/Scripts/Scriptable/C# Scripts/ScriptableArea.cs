using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Area", menuName = "Torrette/Area")]
public class ScriptableArea : ScriptableTorretta
{
    public float raggio_impatto;
    private float timeRemaining;
    public override void Shoot(Transform torretta, Transform target, float actualDamage, float actualFire_rate)
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

            Collider[] colliders = Physics.OverlapSphere(target.position, raggio_impatto);
            foreach(Collider enemy in colliders)
            {
                enemy.TryGetComponent<Entity>(out Entity enemyScript);
                if (enemyScript != null)
                    enemyScript.PV -= actualDamage;
            }
            #endregion

            timeRemaining = actualFire_rate;
        }
    }
}