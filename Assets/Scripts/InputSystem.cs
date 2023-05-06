using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputSystem : MonoBehaviour
{

    #region Creazione instanza
    public static InputSystem current;
    private void Awake()
    {
        if (current != null)
            Destroy(gameObject);
        else
            current = this;
    }
    #endregion

    #region Variabili tasti
    [Serializable]
    private struct Tasti 
    {
        public KeyCode spawnTurret;
        public KeyCode interactButton;
    }

    [SerializeField] private Tasti tasti;
    #endregion


    public Vector3 ReadMouse(LayerMask mask)
    {
        Ray mouseRay = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(mouseRay, out RaycastHit hit, 100, mask) && hit.transform.gameObject.layer != 5)
        {
            return hit.point;
        }
        else
            return Vector3.zero;
    }

    public GameObject ReadPointedObj(LayerMask mask)
    {
        Ray mouseRay = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(mouseRay, out RaycastHit hit, 100, mask))
            return hit.transform.gameObject;
        
        return null;
    }

    public bool ReadInteractButton()
    {
        return Input.GetKey(tasti.interactButton);
    }

    public bool ReadInteractButtonUp()
    {
        return Input.GetKeyUp(tasti.interactButton);
    }

    public bool ReadSpawnTurret()
    {
        return Input.GetKeyDown(tasti.spawnTurret);
    }
}
