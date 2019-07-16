using System.Collections.Generic;
using System.Linq;
using SubjectNerd.Utilities;
using UnityEngine;

[CreateAssetMenu(menuName = "Content/Data/Location Data")]
public class LocationData : ContentData
{
    [Header("Location")]
    [Reorderable(ReordableNamingType.Variable, "name")]
    public List<SituationChanceToOccur> situations;
    [Reorderable(ReordableNamingType.ScriptableObjectName, "enemyData")]
    public List<EnemySpawnChance> enemies;
    public List<PoiSpawnChance> pointsOfInterest;

    internal List<Sprite> zones;

    // TODO: optimize, to avoid sorting all objects each validation
    // sort ascending by spawn chance, for easier spawning
    void OnEnable()
    {
        // auto load all images from corresponding Zones folder
        zones = Resources.LoadAll<Sprite>($"Locations/{name}/Zones/").ToList();

        situations.Sort((x, y) => y.chance.CompareTo(x.chance));
        situations.Reverse();

        enemies.Sort((x, y) => y.chance.CompareTo(x.chance));
        enemies.Reverse();
    }
}