using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColumnManager : MonoBehaviour
{
    public int maxItems = 4;
    //Parte da uno contando se stesso
    public int ItemInColumn = 0;

    public Torretta[] torrete = new Torretta[4];

    public int AddToColumn(Torretta torretta)
    {
        if (ItemInColumn < maxItems)
        {
            torrete[ItemInColumn] = torretta;
            ItemInColumn++;
            return ItemInColumn;
        }
        else
            return 0;
    }
}
