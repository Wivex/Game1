using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class SpawnOption
{
    [Range(1, 1000),
     Tooltip("Relative value in proportion to sum of other elements weights, determining chance of this element selection")]
    public int relativeChanceWeight = 1;
}


public static class SpawnOptionExtensions
{
    /// <summary>
    /// Returns one random option by it's weighted chance
    /// </summary>
    public static T PickOne<T>(this List<T> options) where T : SpawnOption
    {
        var totalWeight = options.Sum(opt => opt.relativeChanceWeight);
        var roll = Random.Range(1, totalWeight);
        foreach (var opt in options)
        {
            roll -= opt.relativeChanceWeight;
            if (roll <= 0) return opt;
        }

        throw new Exception("Could not pick an option");
    }

    /// <summary>
    /// Returns required number of options by weight.
    /// </summary>
    /// <param name="exclusiveMode">If true, picked elements are excluded from possible pick selection, with weight sum being recalculated after each pick. If false, each pick is independent from another/</param>
    /// <returns></returns>
    public static List<T> PickMany<T>(this List<T> options, int pickCount, bool exclusiveMode = false)
        where T : SpawnOption
    {
        var picks = new List<T>();
        if (!exclusiveMode)
        {
            while (pickCount-- > 0)
            {
                picks.Add(options.PickOne());
            }
        }
        else
        {
            var tempList = new List<T>(options);
            while (pickCount-- > 0 && tempList.Count > 0)
            {
                var opt = options.PickOne();
                picks.Add(opt);
                options.Remove(opt);
            }
        }

        return picks;
    }

    /// <summary>
    /// Returns one random value by weighted chance from options
    /// </summary>
    public static List<float> ToProbabilityList<T>(this List<T> weightList) where T : SpawnOption
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