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
    internal int CurValue { get; set; }

    internal StatDepletable(int value) : base(value)
    {
        CurValue = value;
    }
}