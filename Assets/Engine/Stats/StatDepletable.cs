using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

/// <summary>
/// Used for HP, Energy representation (base, max, cur values)
/// </summary>
[Serializable]
internal class StatDepletable : Stat
{
    /// <summary>
    /// Free to change current value. Doesn't need recalculation after change
    /// </summary>
    int curValue;
    internal int CurValue
    {
        get => curValue;
        //limited to range of (0, ModdedValue)
        set => curValue = Mathf.Clamp(value, 0, ModdedValue);
    }

    internal StatDepletable(int value) : base(value)
    {
        CurValue = value;
    }

    protected override void NewModdedValue()
    {
        base.NewModdedValue();
        //can't be more than new ModdedValue
        CurValue = Mathf.Min(CurValue, ModdedValue);
    }
}