using System;
using System.Collections;
using DG.Tweening;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.XR;
using UnityEditor.UI;

public class PlaceableSystem : MonoBehaviour
{
    [SerializeField] private Tilemap main_tilemap;

    private GameObject selectedObj;
    [SerializeField] private Tile previewTile;

    [SerializeField] private List<GameObject> placeableObjects;
    [SerializeField] private LayerMask placeableMask;
    [SerializeField] private LayerMask objMask;

    #region Preview location
    private Vector3Int cellPosition;
    public Vector3 GetCellPosition(Vector3 pos)
    {
        cellPosition = main_tilemap.WorldToCell(pos);
        return main_tilemap.GetCellCenterWorld(cellPosition);
    }
    #endregion


    private Vector3 previousCellLocation;
    private void Update()
    {
        #region PreviewTile
        main_tilemap.SetTile(main_tilemap.WorldToCell(previousCellLocation), null);

        var mouseLocation = InputSystem.current.ReadMouse(placeableMask);
        if (mouseLocation != Vector3.zero)
        {
            previousCellLocation = GetCellPosition(InputSystem.current.ReadMouse(placeableMask));
            main_tilemap.SetTile(main_tilemap.WorldToCell(previousCellLocation), previewTile);
        }
        #endregion

        if (InputSystem.current.ReadInteractButton() && selectedObj != null)
        {
            FollowMouse();   
        }
        else if (InputSystem.current.ReadInteractButtonUp() && selectedObj != null)
        {
            /*
            if(GetCellPosition(InputSystem.current.ReadMouse(placeableMask)) != Vector3.zero)
            {
            }
            */

            //Snap
            selectedObj.transform.position = GetCellPosition(InputSystem.current.ReadMouse(placeableMask) + Vector3.up);

            selectedObj = null;
        }
    }
    
    //Detecta se sulla cella puntata si trova già un'oggetto posizionato
    /*private bool ObjectDetection()
    {
        return Physics.
    }
    */

    public void SetTorretta(GameObject torretta)
    {
        selectedObj = Instantiate(torretta, GetCellPosition(InputSystem.current.ReadMouse(placeableMask)), Quaternion.identity);
    }

    private void FollowMouse()
    {
        selectedObj.transform.position =  GetCellPosition(InputSystem.current.ReadMouse(placeableMask) + Vector3.up);
        //Debug.LogWarning(GetCellPosition(InputSystem.current.ReadMouse(placeableMask) + Vector3.up));
    }

    private void PlaceObj()
    {
        selectedObj.transform.DOMove(GetCellPosition(InputSystem.current.ReadMouse(placeableMask)), 0.5f);
    }

    private void ShowPreviewObj()
    {
        
    }

}
