using System;
using System.Collections.Generic;
using UnityEngine;

public enum EquipmentSlot
{
    None,
    Head,
    Body,
    Arms,
    Boots,
    Amulet,
    Belt,
    Ring1,
    Ring2,
    MainHand1,
    OffHand1,
    MainHand2,
    OffHand2
}

[Serializable]
public class Equipment
{
    public Item head,
        body,
        arms,
        boots,
        amulet,
        belt,
        ring1,
        ring2,
        mainHand1,
        offHand1,
        mainHand2,
        offHand2;
}