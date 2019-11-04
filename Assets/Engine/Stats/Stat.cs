using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[Serializable]
internal class Stat
{
    protected List<StatModifier> mods = new List<StatModifier>();

    /// <summary>
    /// Auto-property (integrated variable). Can only be set in constructor.
    /// </summary>
    internal int BaseValue { get; }
    /// <summary>
    /// Base value after all mods are applied. Any new mod apply recalculation of this value.
    /// </summary>
    internal int ModdedValue { get; private set; }

    internal Stat(int value)
    {
        BaseValue = value;
        ModdedValue = BaseValue;
    }

    protected virtual void NewModdedValue()
    {
        ModdedValue = BaseValue;
        foreach (var mod in mods)
        {
            ModdedValue += mod.value;
        }
        //can't be less than 0
        ModdedValue = Mathf.Max(0, ModdedValue);
    }

    // NOTE: should check if already added
    internal void TryAddModifier(StatModifier mod)
    {
        mods.Add(mod);
        NewModdedValue();
    }

    // And change the RemoveModifier method
    internal void RemoveModifier(StatModifier mod)
    {
        mods.Remove(mod);
        NewModdedValue();
    }
}