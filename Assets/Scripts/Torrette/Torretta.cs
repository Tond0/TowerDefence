using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Torretta : MonoBehaviour
{
    private enum TipoTorretta { ColpoSingolo, Area, Mitragliatrice }
    private TipoTorretta tipoTorretta;

    private IShoot shootingStyle;
    // Start is called before the first frame update
    void Start()
    {
        #region Assegnamento shooting style
        switch (tipoTorretta) 
        {
            case TipoTorretta.ColpoSingolo:
                shootingStyle = new ColpoSingolo();
                    break;

            case TipoTorretta.Area:
                shootingStyle = new Area();
                break;

            case TipoTorretta.Mitragliatrice:
                shootingStyle = new Mitragliatrice();
                break;
        }
        #endregion
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
