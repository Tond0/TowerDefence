using System;
using System.Collections;
using DG.Tweening;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEditor;

public class PlaceableSystem : MonoBehaviour
{
    [SerializeField] private Tilemap main_tilemap;
    [SerializeField] private Grid main_palette;

    [SerializeField] private GameObject torretta;
    [SerializeField] private GameObject buffer;

    [SerializeField] private GameObject selectedObj;
    [SerializeField] private Tile previewTile;
    [SerializeField] private Tile pathTile;

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

    public void PaintCell(Tile newTile)
    {
        main_tilemap.SetTile(cellPosition, newTile);
    }
    #endregion

    private void OnEnable()
    {
        #region SetUpPreviewObj
        placeableObjects.Insert(0, null);

        //selectedObj = placeableObjects[0];
        #endregion
    }

    private Vector3 previousCellLocation;
    private void Update()
    {
        //Debug.Log(main_tilemap.getce(Vector3Int.FloorToInt(InputSystem.current.ReadMouse(placeableMask))));

        if (main_tilemap.GetTile(Vector3Int.FloorToInt(InputSystem.current.ReadMouse(placeableMask)))) return;

        #region PreviewTile
        main_tilemap.SetTile(main_tilemap.WorldToCell(previousCellLocation), null);

        previousCellLocation = GetCellPosition(InputSystem.current.ReadMouse(placeableMask));

        main_tilemap.SetTile(main_tilemap.WorldToCell(previousCellLocation), previewTile);
        #endregion

        if (InputSystem.current.ReadInteractButton() && selectedObj != null)
        {
            FollowMouse();
        }
        else if (InputSystem.current.ReadInteractButtonUp() && selectedObj != null)
        {

            if (GetCellPosition(InputSystem.current.ReadMouse(placeableMask)) != Vector3.zero)
            {
                selectedObj.GetComponent<Torretta>().enabled = true;
            }

            //Snap
            selectedObj.transform.position = InputSystem.current.ReadMouse(placeableMask) + Vector3.up;

            selectedObj = null;
        }
        
    }

    public void SetTorretta(ScriptableTorretta torrettaType)
    {
        selectedObj = Instantiate(torretta, GetCellPosition(InputSystem.current.ReadMouse(placeableMask)), Quaternion.identity);
        selectedObj.GetComponent<Torretta>().BehaviourTorretta = torrettaType;
    }

    private void FollowMouse()
    {
        //selectedObj.transform.DOMove(GetCellPosition(InputSystem.current.ReadMouse(placeableMask)), 0.1f, true);
        selectedObj.transform.position = GetCellPosition(InputSystem.current.ReadMouse(placeableMask));
    }

    private void PlaceObj()
    {
        selectedObj.transform.DOMove(GetCellPosition(InputSystem.current.ReadMouse(placeableMask)), 0.5f);
    }

    private void ShowPreviewObj()
    {
        
    }

}
