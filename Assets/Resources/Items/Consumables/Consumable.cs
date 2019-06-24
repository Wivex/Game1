using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[Serializable]
public class Consumable : Item
{
    public ConsumableData consumableData;
    public int curCharges;

    public Consumable(ConsumableData consumableData)
    {
        this.consumableData = consumableData;
        curCharges = consumableData.charges;
    }
}