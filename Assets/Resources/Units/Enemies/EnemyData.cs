using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(menuName = "Content/Data/Enemy Data")]
public class EnemyData : UnitData
{
    [Header("Enemy Properties")]
    public List<LootData> lootTable;
    [HideInInspector]
    public Sprite sprite;

#if UNITY_EDITOR
    internal override void OnValidate()
    {
        base.OnValidate();

        sprite = AssetHandler.LoadNearbyAssetWithSameName<Sprite>(this);
        // TODO: optimize, to avoid sorting all objects each validation
        // sort ascending by drop chance, for easier loot spawning
        lootTable?.Sort((x, y) => y.dropChance.CompareTo(x.dropChance));
        lootTable?.Reverse();
    }
#endif
}