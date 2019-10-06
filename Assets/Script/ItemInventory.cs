using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ItemInventory
{
    public Item item;
    public int number = 1;

    public ItemInventory(Item item, int number = 1)
    {
        this.item = item;
        this.number = number;
    }
}
