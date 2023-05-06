using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Torretta : MonoBehaviour
{
    public enum TipoTorretta { ColpoSingolo, Area, Mitragliatrice }
    public TipoTorretta tipoTorretta;

    [SerializeField] private GameObject proiettile;
    [SerializeField] private MeshRenderer mesh;
    [SerializeField] private float danno;
    [SerializeField] private float raggio;
    [SerializeField] private float fire_rate;
    [SerializeField] private LayerMask nemicoMask;

    private IShoot shootMode;
    // Start is called before the first frame update
    void Start()
    {
        #region Assegnamento shooting style
        switch (tipoTorretta) 
        {
            case TipoTorretta.ColpoSingolo:
                shootMode = new ColpoSingolo(proiettile, transform);
                    break;

            case TipoTorretta.Area:
                shootMode = new Area();
                break;

            case TipoTorretta.Mitragliatrice:
                shootMode = new Mitragliatrice();
                break;
        }
        #endregion
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(TargetSearch().Length > 0)
            shootMode.Shoot(TargetSearch()[0].transform, fire_rate, danno);
    }

    private Collider[] TargetSearch()
    {
        return Physics.OverlapSphere(transform.position, raggio, nemicoMask);
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, raggio);
    }
}
