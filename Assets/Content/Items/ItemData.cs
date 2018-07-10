using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemData : ContentData
{
    [Header("Item")]
    public bool stackable;
    [DisabledIfNot("stackable")]
    public int maxValue;
    [HiddenIfNot("stackable")]
    public int maxStackSize;
}
