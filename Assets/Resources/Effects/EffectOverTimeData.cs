using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class EffectOverTimeData
{
    public EffectOverTimeType type;
    public TargetType target;
    public int duration, delay;

    [HideInInspector]
    public string effectName;
}