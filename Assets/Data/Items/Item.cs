using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Item : MonoBehaviour
{
    public int cost;
    public int stackSize;
    public int maxStackSize = 1;
}
