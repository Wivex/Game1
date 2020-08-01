using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Content/Data/Container Data")]
public class ContainerData : ScriptableObject
{
    public Sprite icon;
    public List<LootData> lootTable;

    void OnEnable()
    {
        // TODO: optimize, to avoid sorting all objects each validation
        // sort ascending by drop chance, for easier loot spawning
        lootTable?.Sort((x, y) => y.dropChance.CompareTo(x.dropChance));
        lootTable?.Reverse();
    }
}