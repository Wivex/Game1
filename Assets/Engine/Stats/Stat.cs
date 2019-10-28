using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[Serializable]
public class Stat
{
    public StatType type;
    public int baseValue;
    public int curValue;
    public List<StatModifier> statMods;

    public int Value
    {
        get { return curValue; }
        set
        {
            if (value > baseValue)

            curValue = value; 

        }
    }

    public void AddModifier(StatModifier mod)
    {

    }

    public void RemoveModifier(StatModifier mod)
    {
    }
}