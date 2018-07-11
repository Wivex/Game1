using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemData : ContentData
{
    [Header("Item")]
    public bool stackable;
    [HiddenIfNot("stackable")]
    public int maxStackSize = 1;
}
