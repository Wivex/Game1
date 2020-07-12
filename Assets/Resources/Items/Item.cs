using System;
using UnityEngine;
using UnityEditor;

[Serializable]
public class Item
{
    public ItemData data;
    internal int stackSize = 0, charges = 0;

    public Item(ItemData data)
    {
        this.data = data;
    }
}