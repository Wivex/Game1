using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[Serializable]
internal class Stat
{
    protected int maxValue;
    protected bool dirty;
    protected List<StatModifier> mods = new List<StatModifier>();

    internal int BaseValue { get; set; }

    internal int MaxValue
    {
        set
        {
            maxValue = value;
            dirty = true;
        }
        get
        {
            if (dirty)
            {
                RecalculateValues();
                dirty = false;
            }
            return maxValue;
        }
    }

    internal Stat(int value)
    {
        BaseValue = value;
        maxValue = BaseValue;
    }

    void RecalculateValues()
    {
        foreach (var mod in mods)
        {
            MaxValue += mod.value;
        }
    }

    // Change the AddModifier method
    internal void AddModifier(StatModifier mod)
    {
        dirty = true;
        mods.Add(mod);
    }

    // And change the RemoveModifier method
    internal void RemoveModifier(StatModifier mod)
    {
        mods.Remove(mod);
        dirty = true;
    }
}