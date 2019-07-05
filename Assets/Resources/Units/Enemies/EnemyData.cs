using System.Collections.Generic;
using SubjectNerd.Utilities;
using UnityEngine;

[CreateAssetMenu(menuName = "Content/Data/Enemy Data")]
public class EnemyData : UnitData
{
    [Header("Enemy")]
    public float spawnChance;
    public int spawnInterval;
    [Reorderable(ReordableNamingType.ScriptableObjectName, "item")]
    public List<LootData> lootTable;

    // TODO: optimize, to avoid sorting all objects each validation
    // sort ascending by drop chance, for easier loot spawning
    void OnEnable()
    {
        lootTable.Sort((x, y) => y.dropChance.CompareTo(x.dropChance));
        lootTable.Reverse();
    }
}