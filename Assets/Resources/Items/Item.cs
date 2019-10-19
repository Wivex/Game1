using System;
using UnityEngine;
using UnityEditor;

public abstract class Item
{
    internal abstract ItemData Data
    {
        get;
    }

    internal int stackSize;
}