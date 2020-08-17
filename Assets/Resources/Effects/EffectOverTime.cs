using System;
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

    internal void NextTurn(Mission mis, Unit unit)
    {

        if (curDelay > 0)
            curDelay--;
        else
        {
            ApplyEffect(mis, unit);
            if (curDuration-- < 0) unit.effects.Remove(this);
        }
    }
    
    internal void ApplyEffect(Mission mis, Unit unit)
    {
        switch (data.effectName)
        {
            case "Damage":
                mis.ApplyDamage(unit, new Damage(data.type.damageType, data.amount));
                break;
            default:
                Debug.Log($"{data.effectName} is not yet implemented");
                break;
        }
    }
}