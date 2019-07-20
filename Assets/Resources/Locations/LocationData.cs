using System;
using System.Collections.Generic;
using System.Linq;
using MyBox;
//using SubjectNerd.Utilities;
using UnityEngine;

public enum AreaType
{
    Entrance,
    Exit,
    Interchangable,
    Special
}

[Serializable]
public class LocationArea
{
    // not internal cause Reorderable needs it public for renaming of elements
    [HideInInspector]
    public Sprite areaImage;
    public Vector2 areaImageSize;
    public AreaType type;

    [ConditionalField("type", AreaType.Exit)]
    public List<LocationData> connectedLocations;
    public List<Vector2> zonesPositions;
}

[CreateAssetMenu(menuName = "Content/Data/Location Data")]
public class LocationData : ScriptableObject
{
    //[Reorderable(ReordableNamingType.ObjectName, "areaImage")]
    public List<LocationArea> areas;
    //[Reorderable(ReordableNamingType.VariableValue, "name")]
    public List<SituationChanceToOccur> situations;
    //[Reorderable(ReordableNamingType.ObjectName, "enemyData")]
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
        situations.Sort((x, y) => y.chance.CompareTo(x.chance));
        situations.Reverse();

        enemies.Sort((x, y) => y.chance.CompareTo(x.chance));
        enemies.Reverse();
    }
}