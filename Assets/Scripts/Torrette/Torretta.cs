using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Torretta : MonoBehaviour, IPlaceable
{
    public ScriptableTorretta BehaviourTorretta;
    [SerializeField] private LayerMask nemicoMask;
    public Buffer buffer;

    [NonSerialized] public float raggioApp;
    [NonSerialized] public float dannoApp;
    [NonSerialized] public float fire_rateApp;

    private void OnEnable()
    {
        raggioApp = BehaviourTorretta.raggio;
        dannoApp = BehaviourTorretta.danno;
        fire_rateApp = BehaviourTorretta.fire_rate;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (TargetSearch().Length > 0)
        {
            BehaviourTorretta.Shoot(transform, TargetSearch()[0].transform, dannoApp, fire_rateApp);
        }
    }

    private Collider[] TargetSearch()
    {
        if(buffer && buffer.BehaviourBuffer.type == ScriptableBuffer.TipoBuffer.Range)
            return Physics.OverlapSphere(transform.position, BehaviourTorretta.raggio + buffer.BehaviourBuffer.buffValue, nemicoMask);
        else
            return Physics.OverlapSphere(transform.position, BehaviourTorretta.raggio, nemicoMask);
    }
    private void OnDrawGizmos()
    {
        if (BehaviourTorretta != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, raggioApp);
            if(BehaviourTorretta.GetType() == typeof(ScriptableArea)) 
            {
                ScriptableArea areaAppoggio = (ScriptableArea)BehaviourTorretta;
                Gizmos.color = Color.yellow;
                Gizmos.DrawWireSphere(transform.position, areaAppoggio.raggio_impatto);
            }
        }
    }

    private void OnMouseEnter()
    {
        TryGetComponent<MeshRenderer>(out MeshRenderer mesh);
        mesh.material.color = Color.yellow;
    }

    private void OnMouseExit()
    {
        TryGetComponent<MeshRenderer>(out MeshRenderer mesh);
        mesh.material.color = Color.white;
    }
}
