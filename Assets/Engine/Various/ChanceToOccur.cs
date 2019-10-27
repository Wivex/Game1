using System;
using UnityEngine;

public abstract class ChanceToOccur
{
    [Range(0, 1)]
    public float chance;
    //[Tooltip("added to chance after each failed attempt")]
    //public float spawnChanceWeight;
}

[Serializable]
public class EncounterChanceToOccur : ChanceToOccur
{
    public EncounterType type;
    [HideIfNotEnumValues("type", EncounterType.POI)]
    public Sprite icon;
}

[Serializable]
public class EnemySpawnChance : ChanceToOccur
{
    public EnemyData enemyData;
}

[Serializable]
public class EquipmentDropChance : ChanceToOccur
{
    public ItemData itemData;
}

[Serializable]
public class PoiSpawnChance : ChanceToOccur
{
    //public POIData POIData;
}