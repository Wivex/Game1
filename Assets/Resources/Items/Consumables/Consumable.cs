using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[Serializable]
public class Consumable
{
    public ConsumableData consumableData;
    public int curCharges;

    public Consumable(ConsumableData consumableData)
    {
        this.consumableData = consumableData;
        curCharges = consumableData.charges;
    }
}