using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Random = UnityEngine.Random;

[Serializable]
public class RelativeChanceWeight
{
    [Range(1, 1000),
     Tooltip("Relative value in proportion to sum of other elements weights, determining chance of this element selection")]
    public int relativeChanceWeight = 1;
}


public static class RelativeChanceWeightExtensions
{
    /// <summary>
    /// Returns one random value by weighted chance from list
    /// </summary>
    public static int GetOneByWeight(this List<RelativeChanceWeight> list)
    {
        var totalWeight = list.Sum(elem => elem.relativeChanceWeight);
        var roll = Random.Range(1, totalWeight);
        for (var i = 0; i < list.Count; i++)
        {
            roll -= list[i].relativeChanceWeight;
            if (roll <= 0) return i;
        }

        return -1;
    }

    /// <summary>
    /// Returns one random value by weighted chance from list
    /// </summary>
    public static List<float> ToProbabilityList(this List<RelativeChanceWeight> weightList)
    {
        var probabilityList = new List<float>(weightList.Capacity);
        var totalWeight = weightList.Sum(elem => elem.relativeChanceWeight);
        foreach (var elem in weightList)
        {
            probabilityList.Add((float) elem.relativeChanceWeight / totalWeight);
        }

        return probabilityList;
    }
}