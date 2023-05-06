using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Colpo singolo", menuName = "Torrette/Colpo Singolo")]
public class ScriptablesColpoSingolo : ScriptableTorretta
{
    private float timeRemaining;
    public override void Shoot(Transform torretta, Transform target)
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

            timeRemaining = fire_rate;
        }
    }
}
