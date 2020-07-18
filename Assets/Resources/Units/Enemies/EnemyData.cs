using System.Collections.Generic;
using SubjectNerd.Utilities;
using UnityEngine;

[CreateAssetMenu(menuName = "Content/Data/Enemy Data")]
public class EnemyData : UnitData
{
    public Sprite icon;
    [Reorderable(ReorderableNamingType.ReferencedObjectName, "item")]
    public List<LootData> lootTable;

    void OnEnable()
    {
        // TODO: optimize, to avoid sorting all objects each validation
        // sort ascending by drop chance, for easier loot spawning
        lootTable?.Sort((x, y) => y.dropChance.CompareTo(x.dropChance));
        lootTable?.Reverse();
    }
}