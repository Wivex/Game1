using System;
using UnityEngine;
using UnityEditor;

[Serializable]
public class Item
{
    public ItemData data;
    internal int stackSize, charges;

    public Item(ItemData data)
    {
        this.data = data;
    }
}