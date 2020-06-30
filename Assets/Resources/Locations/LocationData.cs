using System;
using System.Collections.Generic;
using System.Linq;
using SubjectNerd.Utilities;
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
    [Reorderable(ReorderableNamingType.ObjectName, "areaImage")]
    public List<LocationArea> areas;
    [Reorderable(ReorderableNamingType.VariableValue, "type")]
    public List<EncounterWeightedChance> encounters;
    [Reorderable(ReorderableNamingType.ObjectName, "enemyData")]
    public List<EnemySpawnWeightedChance> enemies;
    public List<ContainerSpawnWeightedChance> pointsOfInterest;

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
    }
}