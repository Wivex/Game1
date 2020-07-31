using System.Collections.Generic;
using SubjectNerd.Utilities;
using UnityEngine;

[CreateAssetMenu(menuName = "Content/Data/Enemy Data")]
public class EnemyData : UnitData
{
    [Reorderable(ReorderableNamingType.ReferencedObjectName, "item")]
    public List<LootData> lootTable;
    
    internal Sprite sprite;

    void OnEnable()
    {
        sprite = AssetHandler.LoadNearbyAssetWithSameName<Sprite>(this);
        // TODO: optimize, to avoid sorting all objects each validation
        // sort ascending by drop chance, for easier loot spawning
        lootTable?.Sort((x, y) => y.dropChance.CompareTo(x.dropChance));
        lootTable?.Reverse();
    }
}