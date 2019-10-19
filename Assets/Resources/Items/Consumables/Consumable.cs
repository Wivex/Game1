using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[Serializable]
public class Consumable : Item
{
    internal ConsumableData consumableData;
    internal int curCharges;

    internal override ItemData Data => consumableData;

    public Consumable(ConsumableData consumableData)
    {
        this.consumableData = consumableData;
        curCharges = consumableData.charges;
    }

}