using System.Collections;
using UIEventDelegate;
using UnityEngine;

public class StatManager : MonoBehaviour
{
    public static void AddModifier(Unit unit, StatModifier mod)
    {
        //unit.stats[mod.statType].mods.Add(mod);
    }


    public static void RemoveModifier(Unit unit, StatModifier mod)
    {
        // UNDONE: checks?
        //unit.stats[mod.statType].mods.Remove(mod);
    }

    void DoDamage(Unit unit, int val)
    {
        unit.stats[StatType.Health].CurValue -= val;
    }
}