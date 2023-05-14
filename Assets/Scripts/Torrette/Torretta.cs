using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Torretta
{
    readonly Transform transformObj;
    readonly ScriptableTorretta BehaviourTorretta;
    readonly LayerMask nemicoMask;

    public float raggioApp { get; set; }
    public float dannoApp { get; set; }
    public float fire_rateApp { get; set; }

    public Torretta(Transform transformObj, ScriptableTorretta BehaviourTorretta, LayerMask nemicoMask)
    {
        this.transformObj = transformObj;
        this.BehaviourTorretta = BehaviourTorretta;
        this.nemicoMask = nemicoMask;

        raggioApp = BehaviourTorretta.raggio;
        dannoApp = BehaviourTorretta.danno;
        fire_rateApp = BehaviourTorretta.fire_rate;
    }

    public void DoActive()
    {
        if (TargetSearch().Length > 0)
        {
            BehaviourTorretta.Shoot(transformObj, TargetSearch()[0].transform, dannoApp, fire_rateApp);
        }
    }

    private Collider[] TargetSearch()
    {
        return Physics.OverlapSphere(transformObj.position, raggioApp, nemicoMask);
    }
}
