using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(menuName = "Content/Data/Enemy Data")]
public class EnemyData : UnitData
{
    public List<LootData> lootTable;
    [HideInInspector]
    public Sprite sprite;
    
#if UNITY_EDITOR
    void OnValidate()
    {
        sprite = AssetHandler.LoadNearbyAssetWithSameName<Sprite>(this);
        // TODO: optimize, to avoid sorting all objects each validation
        // sort ascending by drop chance, for easier loot spawning
        lootTable?.Sort((x, y) => y.dropChance.CompareTo(x.dropChance));
        lootTable?.Reverse();

        // required to be able to save script changes to SO to an actual asset file (only inspector changes are saved by default)
        EditorUtility.SetDirty(this);
    }
#endif
}