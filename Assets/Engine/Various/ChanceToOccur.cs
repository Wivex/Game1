﻿using System;
using UnityEngine;

public abstract class ChanceToOccur
{
    [Range(0, 1)]
    public float chance;
    //[Tooltip("added to chance after each failed attempt")]
    //public float spawnChanceWeight;
}

[Serializable]
public class SituationChanceToOccur : ChanceToOccur
{
    public string name;
    public Sprite icon;
    public SituationType SituationType;
}

[Serializable]
public class EnemySpawnChance : ChanceToOccur
{
    public EnemyData enemyData;
}

[Serializable]
public class EquipmentDropChance : ChanceToOccur
{
    public EquipmentData equipmentData;
}

[Serializable]
public class PoiSpawnChance : ChanceToOccur
{
    //public POIData POIData;
}