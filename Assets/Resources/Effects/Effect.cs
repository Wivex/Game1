using System;
using UnityEngine;

internal class Effect
{
    internal EffectParams @params;
    internal int curDuration, curDelay;

    internal Effect(EffectParams @params)
    {
        this.@params = @params;
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
        switch (@params.effectName)
        {
            //case "Damage":
            //    mis.ApplyDamage(unit, new Damage(@params.damageType, @params.amount));
            //    break;
            //case EffectType.Healing:
            //    break;
            //case EffectType.EnergyGain:
            //    break;
            //case EffectType.EnergyLoss:
            //    break;
            //case EffectType.StatModifier:
            //    break;
        }
    }
}