using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class EncounterSpawnOption : SpawnOption
{
    public EncounterType type;
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



[CreateAssetMenu(menuName = "Content/Data/Zone Data")]
public class ZoneData : ScriptableObject
{
    public List<Area> areas;
    public List<EncounterSpawnOption> encounters;
    public List<EnemySpawnOption> enemies;

#if UNITY_EDITOR
    void OnValidate()
    {
        // auto-load all location sprites in each area
        foreach (var area in areas)
        {
            area.locations = new List<Sprite>(AssetHandler.LoadChildAssets<Sprite>(area.areaTexture));
        }
        // sort ascending enemies spawn list by chance of spawn
        enemies = enemies?.OrderBy(x => x.relativeChanceWeight).ToList();

        // required to be able to save script changes to SO to an actual asset file (only inspector changes are saved by default)
        EditorUtility.SetDirty(this);
    }
#endif
}