using System;
using System.Collections;
using DG.Tweening;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEditor;

public class PlaceableSystem : MonoBehaviour
{
    [Header("Tilemap Settings")]
    [SerializeField] private Tilemap main_tilemap;
    [SerializeField] private Tile previewTile;

    [Header("Prefabs Settings")]
    [SerializeField] private GameObject columnManger;
    [SerializeField] private GameObject placeableObj;

    [Header("Masks Settings")]
    [SerializeField] private LayerMask groundMask;
    [SerializeField] private LayerMask objMask;
    [SerializeField] private LayerMask columnMask;

    [Header("Debug Settings")]
    [SerializeField] private GameObject selectedObj;

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

    private Vector3 previousCellLocation;
    private void Update()
    {
        #region PreviewTile
        main_tilemap.SetTile(main_tilemap.WorldToCell(previousCellLocation), null);

        if(InputSystem.current.ReadMouse(groundMask) != Vector3.zero)
            previousCellLocation = GetCellPosition(InputSystem.current.ReadMouse(groundMask));

        main_tilemap.SetTile(main_tilemap.WorldToCell(previousCellLocation), previewTile);
        #endregion

        #region Mentre il tasto SX del mouse è giù
        if (InputSystem.current.ReadInteractButton() && selectedObj != null)
        {
            FollowMouse();
        }
        #endregion

        #region Quando rilascia il tasto SX del mouse
        else if (InputSystem.current.ReadInteractButtonUp() && selectedObj != null)
        {
            #region Se può piazzare in quella cella...
            if (InputSystem.current.ReadMouse(groundMask) != Vector3.zero)
            {
                //Cerchiamo di ottenere il columnManager presente in quella cella
                ColumnManager colonna = IsCellOccupied(GetCellPosition(InputSystem.current.ReadMouse(groundMask)));

                selectedObj.TryGetComponent<Placeable>(out Placeable placeableScript);
                
                //Se c'è già una columnManager (c'è già uno o più oggetti posizionabili in quella cella)
                if (colonna != null)
                {
                    Vector3 posInColumn = new Vector3(0, colonna.AddToColumn(placeableScript.torretta) - 1, 0);

                    //Se può essere messo in colonna
                    if (posInColumn.y != -1)
                    {
                        //Snap
                        selectedObj.transform.SetParent(colonna.gameObject.transform);
                        selectedObj.transform.localPosition = Vector3.zero + posInColumn;

                        /* TODO: Non ricordo a cosa serviva
                        if (buffer)
                            buffer.orderInColumn = colonna.ItemInColumn - 1;
                        */
                    }
                    //Se non c'è più spazio allora si annulla l'azione
                    else
                    {
                        Destroy(selectedObj);
                        return;
                    }
                }
                //Se non ci sono altri blocchi e stiamo piazzando una torretta...
                else if(colonna == null && placeableScript.BehaviourTorretta != null)
                {
                    //Spawn del columnManager
                    GameObject spawnedColumn = Instantiate(columnManger, GetCellPosition(InputSystem.current.ReadMouse(groundMask)) + Vector3.up / 2, Quaternion.identity);

                    selectedObj.transform.SetParent(spawnedColumn.transform);

                    spawnedColumn.GetComponent<ColumnManager>().AddToColumn(placeableScript.torretta);

                    selectedObj.transform.localPosition = Vector3.zero;
                }
                else
                {
                    Destroy(selectedObj);
                    return;
                }

                placeableScript.EnablePlaceable();
            }
            #endregion

            #region ...sennò si annulla l'azione
            else
            {
                Destroy(selectedObj);
                return;
            }
            #endregion

            selectedObj = null;
        }
        #endregion
        
    }

    //TODO: Si scrive davvero occupied?
    private ColumnManager IsCellOccupied(Vector3 origin)
    {
        Debug.DrawLine(origin, origin + Vector3.up * 5, Color.black, 10);
        if (Physics.Raycast(origin, Vector3.up, out RaycastHit raycastHit, 2, columnMask))
        {
            return raycastHit.transform.GetComponent<ColumnManager>();
        }
        return null;
    }

    public void SetTorretta(ScriptableTorretta torrettaType)
    {
        selectedObj = Instantiate(placeableObj, GetCellPosition(InputSystem.current.ReadMouse(groundMask)), Quaternion.identity);
        selectedObj.GetComponent<Placeable>().BehaviourTorretta = torrettaType;
        selectedObj.GetComponent<Placeable>().DoSetUp();
    }
    public void SetBuffer(ScriptableBuffer bufferType)
    {
        selectedObj = Instantiate(placeableObj, GetCellPosition(InputSystem.current.ReadMouse(groundMask)), Quaternion.identity);
        selectedObj.GetComponent<Placeable>().BehaviourBuffer = bufferType;
        selectedObj.GetComponent<Placeable>().DoSetUp();
    }

    private void FollowMouse()
    {
        //Non era preciso purtroppo T.T
        //selectedObj.transformObj.DOMove(GetCellPosition(InputSystem.current.ReadMouse(placeableMask)), 0.1f, true);

        if(InputSystem.current.ReadMouse(groundMask) != Vector3.zero)
            selectedObj.transform.position = GetCellPosition(InputSystem.current.ReadMouse(groundMask)) + Vector3.up * 5;
    }
}
