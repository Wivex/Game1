using System;
using UnityEngine;

public abstract class ChanceWeight
{
    [Range(1, 1000)] 
    public int chanceWeight = 1;
}

[Serializable]
public class EncounterWeightedChance : ChanceWeight
{
    public EncounterType type;
    [HideIfNotEnumValues("type", EncounterType.POI)]
    public Sprite icon;
}

[Serializable]
public class EnemySpawnWeightedChance : ChanceWeight
{
    public EnemyData enemyData;
}

[Serializable]
public class EquipmentDropWeightedChance : ChanceWeight
{
    public ItemData itemData;
}

[Serializable]
public class ContainerSpawnWeightedChance : ChanceWeight
{
    public ContainerData ContainerData;
}