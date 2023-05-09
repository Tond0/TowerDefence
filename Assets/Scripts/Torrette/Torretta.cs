using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Torretta : MonoBehaviour
{
    public ScriptableTorretta BehaviourTorretta;
    [SerializeField] private LayerMask nemicoMask;

    // Update is called once per frame
    void FixedUpdate()
    {
        if (TargetSearch().Length > 0)
            BehaviourTorretta.Shoot(transform, TargetSearch()[0].transform);
    }

    private Collider[] TargetSearch()
    {
        return Physics.OverlapSphere(transform.position, BehaviourTorretta.raggio, nemicoMask);
    }
    private void OnDrawGizmos()
    {
        if (BehaviourTorretta != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, BehaviourTorretta.raggio);
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
