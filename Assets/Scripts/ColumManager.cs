using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColumManager : MonoBehaviour
{
    [SerializeField] private IPlaceable[] placeables = new IPlaceable[4];
    public int maxItem;
    //Parte da uno contando se stesso
    private int ItemInColumn = 1;

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
