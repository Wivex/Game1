using System.Collections.Generic;
using SubjectNerd.Utilities;
using UnityEngine;

[CreateAssetMenu(menuName = "Content/Data/Enemy Data")]
public class EnemyData : UnitData
{
    [Reorderable(ReorderableNamingType.ObjectName, "item")]
    public List<LootData> lootTable;

    // TODO: optimize, to avoid sorting all objects each validation
    // sort ascending by drop chance, for easier loot spawning
    void OnEnable()
    {
        Debug.Log($"{name} enabled again");
        lootTable?.Sort((x, y) => y.dropChance.CompareTo(x.dropChance));
        lootTable?.Reverse();
    }

    void OnValidate()
    {
        Debug.Log($"{name} validated again");
    }
}