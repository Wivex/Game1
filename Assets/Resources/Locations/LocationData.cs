using System.Collections.Generic;
using SubjectNerd.Utilities;
using UnityEngine;

[CreateAssetMenu(menuName = "Content/Data/Location Data")]
public class LocationData : ContentData
{
    [Header("Location")]
    //public LocationType type;
    [Reorderable(ReordableNamingType.Variable, "name")]
    public List<SituationChanceToOccur> situations;
    [Reorderable(ReordableNamingType.ScriptableObjectName, "enemyData")]
    public List<EnemySpawnChance> enemies;
    public List<PoiSpawnChance> pointsOfInterest;
    
    // TODO: optimize, to avoid sorting all objects each validation
    // sort ascending by spawn chance, for easier spawning
    void OnEnable()
    {
        situations.Sort((x, y) => y.chance.CompareTo(x.chance));
        situations.Reverse();

        enemies.Sort((x, y) => y.chance.CompareTo(x.chance));
        enemies.Reverse();
    }
}