using Game;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu]
public class DataBase : ScriptableObject
{
    [Serializable]
    private struct ClassNameToClassStats
    {
        public string className;
        public UnitClassStats stats;
    }

    [SerializeField] private List<ClassNameToClassStats> classNameToStats;

    private static DataBase instance;

    public void Init() { instance = this; }

    public static UnitStats GetUnitBaseStats(string className, int level)
    {
        var entry = instance.classNameToStats.FirstOrDefault(c => c.className == className);
        return entry.stats.StatsPerLevel[level];
    }
}
