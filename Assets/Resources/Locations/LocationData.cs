using System.Collections.Generic;
using SubjectNerd.Utilities;
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
    [Reorderable("Situation")]
    public List<SituationChanceToOccur> situations;
    [Reorderable("Enemy")]
    public List<EnemySpawnChance> enemies;
    [Reorderable("POI")]
    public List<PoiSpawnChance> pointsOfInterest;
}