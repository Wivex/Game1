using System;
using System.Collections.Generic;
//using SubjectNerd.Utilities;
using UnityEngine;

public enum LootAmountType
{
    Fixed,
    Range
}

[Serializable]
public class LootData
{
    [Range(0,1)]
    public float dropChance;
    public LootAmountType lootAmountType;
    [HideIfNotEnumValues("lootAmountType", LootAmountType.Fixed)]
    public int fixedAmount = 1;
    [HideIfNotEnumValues("lootAmountType", LootAmountType.Range)]
    public int minAmount = 1;
    [HideIfNotEnumValues("lootAmountType", LootAmountType.Range)]

    public int maxAmount = 3;
    public ItemData item;
}