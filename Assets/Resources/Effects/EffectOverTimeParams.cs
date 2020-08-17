using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class EffectOverTimeParams
{
    public EffectOverTimeType effectType;
    public TargetType target;
    public int duration, delay;
    public int amount;

    [HideInInspector]
    public string effectName;
}