using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[Serializable]
public class Equipment : Item
{
    public EquipmentData equipmentDataData;

    public Equipment(EquipmentData equipmentDataData)
    {
        this.equipmentDataData = equipmentDataData;
        stackSize = 1;
    }
}