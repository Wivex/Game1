using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Action
{
    public StatType stat;
    [Range(0, 100)]
    public int statPercent;
    public AbilityData abilityData;
}