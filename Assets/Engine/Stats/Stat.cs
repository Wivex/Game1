using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[Serializable]
internal class Stat
{
    int curValue;
    bool dirty;
    List<StatModifier> mods = new List<StatModifier>();

    internal int BaseValue { get; set; }

    internal int CurValue
    {
        set
        {
            curValue = value;
            dirty = true;
        }
        get
        {
            if (dirty)
            {
                RecalculateValues();
                dirty = false;
            }
            return curValue;
        }
    }

    internal Stat(int value)
    {
        BaseValue = value;
        curValue = BaseValue;
    }

    void RecalculateValues()
    {
        foreach (var mod in mods)
        {
            CurValue += mod.value;
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