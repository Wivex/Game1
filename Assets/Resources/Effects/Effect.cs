using System;
using UnityEngine;

internal class Effect
{
    internal EffectData data;
    internal int curDuration, curDelay;

    internal Effect(EffectData data)
    {
        this.data = data;
    }

    internal void NextTurn(Mission mis, Unit unit)
    {

        if (curDelay > 0)
            curDelay--;
        else
        {
            ProcEffect(mis, unit);
            if (curDuration-- < 0) unit.effects.Remove(this);
        }
    }
    
    internal void ProcEffect(Mission mis, Unit unit)
    {
        switch (data.effectType)
        {
            case EffectType.Damage:
                mis.ApplyDamage(unit, new Damage(data.damageType, data.amount));
                break;
            case EffectType.Healing:
                break;
            case EffectType.EnergyGain:
                break;
            case EffectType.EnergyLoss:
                break;
            case EffectType.StatModifier:
                break;
        }
    }
}