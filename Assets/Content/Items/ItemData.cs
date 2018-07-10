using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemData : ContentData
{
    [Header("Item")]
    public bool stackable;
    [ConditionalHide("stackable", true)]
    public int maxStackSize;
    [ConditionalHide("stackable", true)]
    public List<int> opts;
}
