using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Placeable : MonoBehaviour
{
    private Vector3 offset;

    private void OnMouseDown()
    {
        offset = transform.position - PlacingSystem.GetMouseWorldPosition();
    }

    private void OnMouseDrag()
    {
        Vector3 pos = PlacingSystem.GetMouseWorldPosition() + offset;
        transform.position = PlacingSystem.current.SnapCoordinateToGrid(pos);
    }
}
