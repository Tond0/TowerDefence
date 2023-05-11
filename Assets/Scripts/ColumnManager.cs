using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColumnManager : MonoBehaviour
{
    [SerializeField] private IPlaceable[] placeables = new IPlaceable[4];
    public int maxItem = 4;
    //Parte da uno contando se stesso
    private int ItemInColumn = 0;

    public int AddToColumn()
    {
        if (ItemInColumn < maxItem)
        {
            ItemInColumn++;
            return ItemInColumn;
        }
        else
            return 0;
    }
}
