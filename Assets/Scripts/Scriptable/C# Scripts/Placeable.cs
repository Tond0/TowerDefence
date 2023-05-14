using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Placeable : MonoBehaviour
{
    [NonSerialized] public ScriptableTorretta BehaviourTorretta;
    [NonSerialized] public ScriptableBuffer BehaviourBuffer;

    [NonSerialized] public Torretta torretta;
    [NonSerialized] public Buffer buffer;

    [NonSerialized] public int orderInColum;

    [Header("Layer del nemico")]
    [SerializeField] private LayerMask nemicoMask;

    public void DoSetUp()
    {
        if (BehaviourTorretta)
            torretta = new Torretta(transform, BehaviourTorretta, nemicoMask);
        else
            buffer = new Buffer(transform, BehaviourBuffer, nemicoMask);
    }


    private void OnDisable()
    {
        if (buffer == null) return;

        buffer.BuffDebuff(StatusBuffer.Disattiva);
    }

    public void EnablePlaceable()
    {
        if(torretta != null)
            canShoot = true;
        else
            buffer.BuffDebuff(StatusBuffer.Attiva);
    }

    private bool canShoot;

    private void FixedUpdate()
    {
        if (torretta == null || !canShoot) return;

        torretta.DoActive();
    }

    #region Preview
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

    #endregion

    /*private void OnDrawGizmos()
    {
        if (BehaviourTorretta != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, torretta.raggioApp);
            if (BehaviourTorretta.GetType() == typeof(ScriptableArea))
            {
                ScriptableArea areaAppoggio = (ScriptableArea)BehaviourTorretta;
                Gizmos.color = Color.yellow;
                Gizmos.DrawWireSphere(transform.position, areaAppoggio.raggio_impatto);
            }
        }
    }*/
}
