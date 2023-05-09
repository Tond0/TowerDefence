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
        #region PreviewTile
        main_tilemap.SetTile(main_tilemap.WorldToCell(previousCellLocation), null);

        if(InputSystem.current.ReadMouse(placeableMask) != Vector3.zero)
            previousCellLocation = GetCellPosition(InputSystem.current.ReadMouse(placeableMask));

        main_tilemap.SetTile(main_tilemap.WorldToCell(previousCellLocation), previewTile);
        #endregion

        if (InputSystem.current.ReadInteractButton() && selectedObj != null)
        {
            FollowMouse();
        }
        else if (InputSystem.current.ReadInteractButtonUp() && selectedObj != null)
        {
            if (InputSystem.current.ReadMouse(placeableMask) != Vector3.zero)
            {
                selectedObj.GetComponent<Torretta>().enabled = true;
                if (IsCellOccupied(GetCellPosition(InputSystem.current.ReadMouse(placeableMask))) != null)
                {
                    ColumManager colonna = IsCellOccupied(GetCellPosition(InputSystem.current.ReadMouse(placeableMask)));

                    Vector3 posInColumn = new Vector3(1, colonna.AddToColumn(), 1);
                    //Se può essere messo in colonna
                    if (posInColumn.y == 0)
                    {
                        //Snap
                        selectedObj.transform.position = GetCellPosition(InputSystem.current.ReadMouse(placeableMask)) + posInColumn;
                    }
                }
                else
                {
                    //Snap
                    selectedObj.transform.position = GetCellPosition(InputSystem.current.ReadMouse(placeableMask));
                }
            }
            else
            {
                Destroy(selectedObj);
            }

            selectedObj = null;
        }
        
    }

    //TODO: Si scrive davvero occupied?
    private ColumManager IsCellOccupied(Vector3 origin)
    {
        if(Physics.Raycast(origin, Vector3.up,out RaycastHit hitObj, 1, objMask))
        {
            hitObj.transform.TryGetComponent<ColumManager>(out ColumManager columManager);
            return columManager;
        }
        return null;
    }

    public void SetTorretta(ScriptableTorretta torrettaType)
    {
        selectedObj = Instantiate(torretta, GetCellPosition(InputSystem.current.ReadMouse(placeableMask)), Quaternion.identity);
        selectedObj.GetComponent<Torretta>().BehaviourTorretta = torrettaType;
    }

    private void FollowMouse()
    {
        //selectedObj.transform.DOMove(GetCellPosition(InputSystem.current.ReadMouse(placeableMask)), 0.1f, true);
        if(InputSystem.current.ReadMouse(placeableMask) != Vector3.zero)
            selectedObj.transform.position = GetCellPosition(InputSystem.current.ReadMouse(placeableMask));
    }

    private void PlaceObj()
    {
        selectedObj.transform.DOMove(GetCellPosition(InputSystem.current.ReadMouse(placeableMask)), 0.5f);
    }
}
