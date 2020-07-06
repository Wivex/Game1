using System;
using System.Collections.Generic;
using System.Linq;
using SubjectNerd.Utilities;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class EncounterSpawnOption : SpawnOption
{
    public EncounterType type;
    // auto-loaded based on encounter type
    internal Sprite interactionIcon;
}

[Serializable]
public class EnemySpawnOption : SpawnOption
{
    public EnemyData enemyData;
}

[Serializable]
public class ContainerSpawnOption : SpawnOption
{
    public ContainerData ContainerData;
}



[Serializable]
public class ZonePath
{
    public ZoneData zoneA, zoneB;
    [Tooltip("Represent number of sites required to traverse in order to reach from zone A to zone B")]
    public int length;
}



[CreateAssetMenu(menuName = "Content/Data/Zone Data")]
public class ZoneData : ScriptableObject
{
    [Reorderable(ReorderableNamingType.VariableValue,"type")]
    public List<Site> sites;

    [Reorderable(ReorderableNamingType.ReferencedObjectName, "zoneB")]
    public List<ZonePath> traversalPaths;

    [Reorderable(ReorderableNamingType.VariableValue, "type")]
    public List<EncounterSpawnOption> encounters;
    [Reorderable(ReorderableNamingType.ReferencedObjectName, "enemyData")]
    public List<EnemySpawnOption> enemies;
    
    void OnEnable()
    {
        // auto-load all sprites from multi-sprite texture of a site
        if (sites.NotNullOrEmpty())
        {
            sites.ForEach(site => site.LoadChainSprites());
        }
    
        // sort ascending enemies spawn list by chance of spawn
        enemies = enemies?.OrderBy(x => x.relativeChanceWeight).ToList();
    }
}