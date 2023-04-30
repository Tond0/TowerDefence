using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PlacingSystem : MonoBehaviour
{
    public static PlacingSystem current;

    public GridLayout gridLayout;
    private Grid grid;

    [SerializeField] private LayerMask mouseableMask;

    [SerializeField] private Tilemap mainTilemap;
    [SerializeField] private TileBase placePreview;

    public GameObject prefab1;
    public GameObject prefab2;

    private Placeable objectToPlace;

    private void Awake()
    {
        if (current != null)
            Destroy(gameObject);
        else
            current = this;

        grid = gridLayout.gameObject.GetComponent<Grid>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            InitializeWithObject(prefab1);
        }
        else if (Input.GetKeyDown(KeyCode.B))
        {
            InitializeWithObject(prefab2);
        }
    }

    public Vector3 GetMouseWorldPosition()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if(Physics.Raycast(ray, out RaycastHit raycastHit, 100, mouseableMask))
        {
            return raycastHit.point;
        }
        else
        {
            return Vector3.zero;
        }
    }

    public Vector3 SnapCoordinateToGrid(Vector3 position)
    {
        Vector3Int cellPos = gridLayout.WorldToCell(position);
        position = grid.GetCellCenterWorld(cellPos);
        return position;
    }

    public void InitializeWithObject(GameObject prefab)
    {
        Vector3 position = SnapCoordinateToGrid(Vector3.zero);

        GameObject obj = Instantiate(prefab, position, Quaternion.identity);
        objectToPlace = obj.GetComponent<Placeable>();
        obj.AddComponent<Placeable>();
    }
}
