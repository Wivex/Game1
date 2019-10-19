using System;
using UnityEngine;

public class ItemData : ScriptableObject
{
    [Header("Item")]
    public Sprite icon;
    public bool stackable;
    [HideIfNotBool("stackable")]
    public int maxStackSize = 1;
    public int cost;
}