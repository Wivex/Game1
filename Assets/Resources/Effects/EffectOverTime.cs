using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

internal class EffectOverTime
{
    internal EffectOverTimeData data;
    internal int curDuration, curDelay;

    internal EffectOverTime(EffectOverTimeData data)
    {
        this.data = data;
    }
}