using UnityEngine;

public enum LocationType
{
    Forest,
    Dungeon
}

[CreateAssetMenu(menuName = "Content/Data/Location Data")]
public class LocationData : ScriptableObject
{
    public new string name;
    public LocationType type;
    public SituationChanceToOccur[] situations;
    public EnemySpawnChance[] enemies;
    public PoiSpawnChance[] pointsOfInterest;
}