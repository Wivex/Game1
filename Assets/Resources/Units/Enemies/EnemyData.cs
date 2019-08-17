﻿using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Content/Data/Enemy Data")]
public class EnemyData : UnitData
{
    [Header("Enemy")]
    public float spawnChance;
    public int spawnInterval;
    //[Reorderable(ReordableNamingType.ObjectName, "item")]
    public List<LootData> lootTable;

    // UNDONE: errors on create new SO
    // TODO: optimize, to avoid sorting all objects each validation
    // sort ascending by drop chance, for easier loot spawning
    void OnEnable()
    {
        //add null or empty check
        //lootTable.Sort((x, y) => y.dropChance.CompareTo(x.dropChance));
        //lootTable.Reverse();
    }
}