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

    [SerializeField] private GameObject columnManger;
    [SerializeField] private GameObject torretta;
    [SerializeField] private GameObject buffer;

    [SerializeField] private GameObject selectedObj;
    [SerializeField] private Tile previewTile;
    [SerializeField] private Tile pathTile;

    [SerializeField] private List<GameObject> placeableObjects;
    [SerializeField] private LayerMask groundMask;
    [SerializeField] private LayerMask objMask;
    [SerializeField] private LayerMask columnMask;

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
        #endregion
    }

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

                //Se c'è già una columnManager (c'è già uno o più oggetti posizionabili in quella cella)
                if (colonna != null)
                {
                    Vector3 posInColumn = new Vector3(0, colonna.AddToColumn(), 0);
                    Debug.Log(posInColumn);
                    //Se può essere messo in colonna
                    if (posInColumn.y != 0)
                    {
                        Debug.Log("Vai sopra");
                        //Snap
                        selectedObj.transform.position = GetCellPosition(InputSystem.current.ReadMouse(groundMask)) + posInColumn + Vector3.up / 2;
                    }
                    else
                    {
                        Destroy(selectedObj);
                    }
                }
                else
                {
                    //Spawn del columnManager
                    GameObject spawnedColumn = Instantiate(columnManger, GetCellPosition(InputSystem.current.ReadMouse(groundMask)) + Vector3.up / 2, Quaternion.identity);
                    selectedObj.transform.SetParent(spawnedColumn.transform);
                    selectedObj.transform.localPosition = Vector3.zero;
                }

                selectedObj.GetComponent<Torretta>().enabled = true;
            }
            #endregion

            #region ...sennò si annulla l'azione
            else
            {
                Destroy(selectedObj);
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
            Debug.Log("Beccato");
            return raycastHit.transform.GetComponent<ColumnManager>();
        }
        Debug.Log("Vuoto");
        return null;
    }

    public void SetTorretta(ScriptableTorretta torrettaType)
    {
        selectedObj = Instantiate(torretta, GetCellPosition(InputSystem.current.ReadMouse(groundMask)), Quaternion.identity);
        selectedObj.GetComponent<Torretta>().BehaviourTorretta = torrettaType;
    }

    private void FollowMouse()
    {
        //selectedObj.transform.DOMove(GetCellPosition(InputSystem.current.ReadMouse(placeableMask)), 0.1f, true);
        if(InputSystem.current.ReadMouse(groundMask) != Vector3.zero)
            selectedObj.transform.position = GetCellPosition(InputSystem.current.ReadMouse(groundMask)) + Vector3.up * 5;
    }

    private void PlaceObj()
    {
        selectedObj.transform.DOMove(GetCellPosition(InputSystem.current.ReadMouse(groundMask)), 0.5f);
    }
}
