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
    
    internal void ApplyEffect(Mission mis, Unit unit)
    {
        switch (data.effectName)
        {
            case "Damage":
                mis.ApplyDamage(unit, new Damage(data.type.damageType, data.type.amount));
                break;
            default:
                Debug.Log($"{data.effectName} is not yet implemented");
                break;
        }
    }
    
    internal void RemoveEffect(Mission mis, Unit unit)
    {
        mis.RemoveEffect(unit, this);
    }
}