using System;
using System.Collections.Generic;
using System.Linq;
using MyBox;
using SubjectNerd.Utilities;
//using SubjectNerd.Utilities;
using UnityEngine;

[Serializable]
public class LocationArea
{
    // not internal cause Reorderable needs it public for renaming of elements
    [HideInInspector] public Sprite areaImage;
    public bool interchangeable;
    public Vector2 areaImageSize;
    public List<LocationData> connectedLocations;
    public List<Vector2> zonesPositions;
}

[CreateAssetMenu(menuName = "Content/Data/Location Data")]
public class LocationData : ScriptableObject
{
    [Reorderable(ReordableNamingType.ObjectName, "areaImage")]
    public List<LocationArea> areas;
    [Reorderable(ReordableNamingType.VariableValue, "eventType")]
    public List<EventChanceToOccur> events;
    [Reorderable(ReordableNamingType.ObjectName, "enemyData")]
    public List<EnemySpawnChance> enemies;
    public List<PoiSpawnChance> pointsOfInterest;

    // NOTE: optimize, to avoid sorting all objects each validation?
    void OnValidate()
    {
        // auto load all images from corresponding Zones folder
        var areaImages = Resources.LoadAll<Sprite>($"Locations/{name}/Areas/").ToList();

        // auto add new areas, based on existing sprites in corresponding "Areas" folder
        foreach (var image in areaImages)
        {
            if (areas.All(area => area.areaImage != image))
                areas.Add(new LocationArea
                {
                    areaImage = image
                });
        }

        // sort ascending by spawn chance, for easier spawning
        events.Sort((x, y) => y.chance.CompareTo(x.chance));
        events.Reverse();

        enemies.Sort((x, y) => y.chance.CompareTo(x.chance));
        enemies.Reverse();
    }
}