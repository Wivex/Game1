using System.Collections.Generic;
using UnityEngine;

public enum LocationType
{
    Forest,
    Dungeon
}

[CreateAssetMenu(menuName = "Content/Data/Location Data")]
public class LocationData : ContentData
{
    [Header("Location")]
    //public LocationType type;
    public List<SituationChanceToOccur> situations;
    public List<EnemySpawnChance> enemies;
    public List<PoiSpawnChance> pointsOfInterest;
}