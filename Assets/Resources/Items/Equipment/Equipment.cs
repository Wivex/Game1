using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[Serializable]
public class Equipment : Item
{
    public EquipmentData equipmentData;

    internal override ItemData Data => equipmentData;

    public Equipment(EquipmentData equipmentData)
    {
        this.equipmentData = equipmentData;
        stackSize = 1;
    }
}